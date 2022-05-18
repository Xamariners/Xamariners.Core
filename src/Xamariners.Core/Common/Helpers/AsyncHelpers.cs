// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncHelpers.cs" company="">
//   
// </copyright>
// <summary>
//   The async helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The async helpers.
    /// </summary>
    public static class AsyncHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines if is async method the specified classType methodName.
        /// </summary>
        /// <returns>
        /// <c>true</c> if is async method the specified classType methodName; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="classType">
        /// Class type.
        /// </param>
        /// <param name="methodName">
        /// Method name.
        /// </param>
        public static bool IsAsyncMethod(Type classType, string methodName)
        {
            MethodInfo method = classType.GetTypeInfo().GetDeclaredMethod(methodName);
            Type attType = typeof(AsyncStateMachineAttribute);
            var attrib = (AsyncStateMachineAttribute)method.GetCustomAttribute(attType);
            return attrib != null;
        }

        /// <summary>
        /// Determines if is event handler async method the specified handler.
        /// </summary>
        /// <returns>
        /// <c>true</c> if is event handler async method the specified handler; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="handler">
        /// Handler.
        /// </param>
        public static bool IsEventHandlerAsyncMethod(Action handler)
        {
            Type classType = handler.GetMethodInfo().DeclaringType;
            string methodName = handler.GetMethodInfo().Name;
            return IsAsyncMethod(classType, methodName);
        }

        /// <summary>
        /// Execute's an async Task of T method which has a void return value synchronously
        /// </summary>
        /// <param name="task">
        /// Task of T method to execute
        /// </param>
        public static void RunSync(Func<Task> task)
        {
            SynchronizationContext oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(
                async t =>
                    {
                        try
                        {
                            await task();
                        }
                        catch (Exception e)
                        {
                            synch.InnerException = e;
                            throw;
                        }
                        finally
                        {
                            synch.EndMessageLoop();
                        }
                    }, 
                null);
            synch.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        /// <summary>
        /// Execute's an async Task of T method which has a T return type synchronously
        /// </summary>
        /// <typeparam name="T">
        /// Return Type
        /// </typeparam>
        /// <param name="task">
        /// Task of T method to execute
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T RunSync<T>(Func<Task<T>> task)
        {
            SynchronizationContext oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            T ret = default(T);
            synch.Post(
                async t =>
                    {
                        try
                        {
                            ret = await task();
                        }
                        catch (Exception e)
                        {
                            synch.InnerException = e;
                            throw;
                        }
                        finally
                        {
                            synch.EndMessageLoop();
                        }
                    }, 
                null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return ret;
        }

        #endregion

        /// <summary>
        ///     ExclusiveSynchronizationContext class
        /// </summary>
        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            #region Fields

            /// <summary>
            ///     Items queue
            /// </summary>
            private readonly Queue<Tuple<SendOrPostCallback, object>> items =
                new Queue<Tuple<SendOrPostCallback, object>>();

            /// <summary>
            ///     Auto reset event
            /// </summary>
            private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);

            /// <summary>
            ///     Done flag
            /// </summary>
            private bool done;

            #endregion

            #region Public Properties

            /// <summary>
            ///     Inner Excepiton
            /// </summary>
            public Exception InnerException { private get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Begin Message Loop
            /// </summary>
            public void BeginMessageLoop()
            {
                while (!done)
                {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (items)
                    {
                        if (items.Count > 0)
                        {
                            task = items.Dequeue();
                        }
                    }

                    if (task != null)
                    {
                        task.Item1(task.Item2);
                        if (InnerException != null)
                        {
                            // the method threw an exeption
                            throw new AggregateException(
                                "AsyncHelpers.Run method threw an exception.", 
                                InnerException);
                        }
                    }
                    else
                    {
                        workItemsWaiting.WaitOne();
                    }
                }
            }

            /// <summary>
            ///     Creates copy of current synchronization context
            /// </summary>
            /// <returns>
            ///     The <see cref="SynchronizationContext" />.
            /// </returns>
            public override SynchronizationContext CreateCopy()
            {
                return this;
            }

            /// <summary>
            ///     End Message Loop
            /// </summary>
            public void EndMessageLoop()
            {
                Post(t => done = true, null);
            }

            /// <summary>
            /// Post to thread
            /// </summary>
            /// <param name="d">
            /// </param>
            /// <param name="state">
            /// </param>
            public override void Post(SendOrPostCallback d, object state)
            {
                lock (items)
                {
                    items.Enqueue(Tuple.Create(d, state));
                }

                workItemsWaiting.Set();
            }

            /// <summary>
            /// Send to thread
            /// </summary>
            /// <param name="d">
            /// </param>
            /// <param name="state">
            /// </param>
            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            #endregion
        }

        public enum RetryResult
        {
            Success,
            Timeout,
            Canceled,
        }

        public static Task<RetryResult> RetryAsync(this Func<Task<bool>> func, CancellationToken cancel, TimeSpan timeOut, int pause = 100)
        {
            return Task.Run(async () =>
            {
                var start = DateTime.UtcNow;
                var end = start + timeOut;

                while (true)
                {
                    var now = DateTime.UtcNow;
                    if (end < now)
                        return RetryResult.Timeout;

                    var curTimeOut = end - now;

                    try
                    {
                        cancel.ThrowIfCancellationRequested();

                        if (cancel.IsCancellationRequested)
                            return RetryResult.Canceled;

                        var curTask = Task.Run(func, cancel);
                        
                         curTask.Wait((int) curTimeOut.TotalMilliseconds);

                        if (curTask.IsCanceled)
                                return RetryResult.Canceled;

                            if (curTask.Result == true)
                                return RetryResult.Success;
                    }
                    catch (TimeoutException)
                    {
                        return RetryResult.Timeout;
                    }
                    catch (TaskCanceledException)
                    {
                        return RetryResult.Canceled;
                    }
                    catch (OperationCanceledException)
                    {
                        return RetryResult.Canceled;
                    }

                    await Task.Delay(pause, cancel);
                }
            }, cancel);
        }

    }
}