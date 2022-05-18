// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The task helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The task helper.
    /// </summary>
    public static class TaskHelper
    {

        /// <summary>
        /// Ases the task.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="value">The value.</param>
        public static Task<TType> AsTask<TType>(this TType value)
        {
            var tcs = new TaskCompletionSource<TType>();
            tcs.SetResult(value);
            return tcs.Task;
        }

        /// <summary>
        /// Starts the new background task.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="backgroundTaskWaitHandle">The background task wait handle.</param>
        public static Task StartNewBackgroundTask(Action action, EventWaitHandle backgroundTaskWaitHandle)
        {
            return Task.Run(
                () =>
                {
                    try
                    {
                        backgroundTaskWaitHandle.Reset();
                        action();
                    }
                    finally
                    {
                        backgroundTaskWaitHandle.Set();
                    }
                });
        }

        /// <summary>
        /// Starts the new princess task.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="princessWaitHandle">The princess wait handle.</param>
        public static Task StartNewPrincessTask(Action action, EventWaitHandle princessWaitHandle = null)
        {
            princessWaitHandle?.Reset();

            return Task.Run(() =>
            {
                try
                {
                    action();
                }
                finally
                {
                    princessWaitHandle?.Set();
                }
            });
        }

        /// <summary>
        /// Starts the new cancellable task.
        /// </summary>
        /// <param name="mainAction">The main action.</param>
        /// <param name="prerequisiteAction">The prerequisite action.</param>
        /// <param name="cts">The CTS.</param>
        /// <param name="padlock">The padlock.</param>
        /// <param name="refreshRate">The refresh rate.</param>
        /// <param name="executeFraction">The execute fraction.</param>
        public static Task StartNewCancellableTask(Action mainAction, Action prerequisiteAction,
            CancellationTokenSource cts, object padlock, int refreshRate, int executeFraction = 10)
        {
            return Task.Run(new Action(async () =>
            {
                prerequisiteAction?.Invoke();

                //initial counter. we want to start right now
                var counter = executeFraction - 1;

                while (cts == null || !cts.IsCancellationRequested)
                {
                    counter++;

                    if (counter%executeFraction == 0)
                    {
                        counter = 0;
                        mainAction();
                    }

                    await Task.Delay((refreshRate/executeFraction)*1000);
                }
            }));
        }
    }
}