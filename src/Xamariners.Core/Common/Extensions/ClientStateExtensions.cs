using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Common.Model;
using Xamariners.Core.Interface;

namespace Xamariners.Core.Common.Extensions
{
    public static class ClientStateExtensions
    {
        public static void Set(this IClientState state, IClientState source)
        {
            foreach (var prop in source.GetType().GetProperties().Where(x => x.CanWrite))
                prop.SetValue(state, prop.GetValue(source));
        }
    }
}
