using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Common.Helpers
{
    public static class ObjectHelpers
    {
        public static T JsonClone<T>(this T source)
        { 
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            try
            {
                // In the PCL we do not have the BinaryFormatter
                var ser = JsonConvert.SerializeObject(source, new JavaScriptDateTimeConverter());
                var deser = JsonConvert.DeserializeObject<T>(ser, new JavaScriptDateTimeConverter());
                return deser;
            }
            catch (Exception ex)
            {
                TraceHelpers.WriteToTrace(ex);
                return source;
            }
        }

        public static T ToType<T>(this object obj)
        {
            return (T)obj;
        }

        public static T ToEnum<T>(this string enumString)
        {
            if (String.IsNullOrEmpty(enumString))
                throw new Exception("Can't parse value to enum because the string is null or empty");

            return (T)System.Enum.Parse(typeof(T), enumString, true);
        }

        public static bool IsDuckTyped<T>(this T source)
        {
            return source.GetType().Name.StartsWith("ActLike_");
        }

		public static bool ValueEquality(object val1, object val2)
		{

			if ((val1 == null && val1 != val2) || (val2 == null && val1 != val2))
				return false;

			if (val1 == null && val2 == null)
				return true;

			// convert val2 to type of val1.
			var converted2 = Convert.ChangeType(val2, val1.GetType());

			// compare now that same type.
			return val1.Equals(converted2);
		}

        //Compare 1 with multiple
        public static bool In<T>(this T obj, params T[] args)
        {
            return args.Contains(obj);
        }
    }
}