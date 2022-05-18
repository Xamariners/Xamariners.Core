using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Interfaces
{
    public interface IParallelHelper
    {
        Task StartNewParallelTask(Action action, bool condition, int refreshRate, int degrees = 5);

        void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body);
    }
}
