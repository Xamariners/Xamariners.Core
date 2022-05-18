using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity
{
    public static class Extensions
    {
        public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source) where T : class
        {
            return source;
        }
    }
}
