using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Common
{
    public static class MyParallel
    {
        public static void While(Func<bool> condition, ParallelOptions parallelOptions, Action action)
        {
            Parallel.ForEach(WhileTrue(condition), parallelOptions, _ => action());
        }

        static IEnumerable<bool> WhileTrue(Func<bool> condition)
        {
            while (condition()) yield return true;
        }
    }
}
