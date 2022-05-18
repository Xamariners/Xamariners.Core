// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   The extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Static Fields

        /// <summary>
        ///     The parameter type projection.
        /// </summary>
        private static readonly Func<MethodInfo, IEnumerable<Type>> ParameterTypeProjection =
            method => method.GetParameters().Select(p => p.ParameterType.GetGenericTypeDefinition());

        #endregion



        #region Public Methods and Operators

        

        public static IEnumerable<T> SelectDeep<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (T item in source)
            {
                yield return item;
                foreach (T subItem in SelectDeep(selector(item), selector))
                {
                    yield return subItem;
                }
            }
        }

        public static IEnumerable<T> SelectDeep<T>(this T source, Func<T, T> selector)
        {
            return new[] { source }.SelectDeep(_ => new[] { selector(_) }.Where(r => !r.Equals(default(T))));
        }

        /// <summary>
        /// </summary>
        /// <param name="dictionary">
        /// </param>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static Dictionary<string, T> AddReplace<T>(this Dictionary<string, T> dictionary, string key, T value)
        {
            if (dictionary == null)
            {
                return null;
            }

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        /// <summary>
        /// </summary>
        /// <param name="dictionary">
        /// </param>
        /// <param name="key">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static IList<T> AddReplace<T>(this IList<T> list, T value)
        {
            if (list == null)
            {
                return null;
            }

            if (!list.Contains(value))
            {
                list.Add(value);
            }

            return list;
        }

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="OriginalEntity">
        /// The original entity.
        /// </param>
        /// <param name="EntityToMergeOn">
        /// The entity to merge on.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public static TEntity CopyTo<TEntity>(this TEntity OriginalEntity, TEntity EntityToMergeOn)
        {
            IEnumerable<PropertyInfo> oProperties = OriginalEntity.GetType().GetProperties();

            foreach (PropertyInfo CurrentProperty in oProperties.Where(p => p.CanWrite))
            {
                object originalValue = CurrentProperty.GetValue(EntityToMergeOn);

                if (originalValue != null)
                {
                    IListLogic(OriginalEntity, CurrentProperty, originalValue);
                }
                else
                {
                    object value = CurrentProperty.GetValue(OriginalEntity, null);
                    CurrentProperty.SetValue(EntityToMergeOn, value, null);
                }
            }

            return OriginalEntity;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        public static IList<T> GetEnumValues<T>()
        {
            return (T[])System.Enum.GetValues(typeof(T));
        }

        /// <summary>
        /// The get generic method.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterTypes">
        /// The parameter types.
        /// </param>
        /// <returns>
        /// The <see cref="MethodInfo"/>.
        /// </returns>
        public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] parameterTypes)
        {
            return (from method in type.GetRuntimeMethods()
                    where method.Name == name
                    where parameterTypes.SequenceEqual(ParameterTypeProjection(method))
                    select method).SingleOrDefault();
        }

        /// <summary>
        /// Search for a method by name, parameter types, and binding flags.  Unlike GetMethod(), does 'loose' matching on
        ///     generic
        ///     parameter types, and searches base interfaces.
        /// </summary>
        /// <param name="thisType">
        /// The this Type.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterTypes">
        /// The parameter Types.
        /// </param>
        /// <exception cref="AmbiguousMatchException">
        /// </exception>
        /// <returns>
        /// The <see cref="MethodInfo"/>.
        /// </returns>
        public static MethodInfo GetMethodExt(this Type thisType, string name, params Type[] parameterTypes)
        {
            MethodInfo matchingMethod = null;

            // Check all methods with the specified name, including in base classes
            GetMethodExt(ref matchingMethod, thisType, name, parameterTypes);

            // If we're searching an interface, we have to manually search base interfaces
            if (matchingMethod == null && thisType.GetTypeInfo().IsInterface)
            {
                foreach (Type interfaceType in thisType.GetTypeInfo().ImplementedInterfaces)
                {
                    GetMethodExt(ref matchingMethod, interfaceType, name, parameterTypes);
                }
            }

            return matchingMethod;
        }

        /// <summary>
        /// The get underlying type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetUnderlyingType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType ? type.GenericTypeArguments[0].GetUnderlyingType() : type;
        }

        /// <summary>
        /// The is nullable.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null)
            {
                return true; // obvious
            }

            Type type = typeof(T);
            if (type.IsByRef)
            {
                return true; // ref-type
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true; // Nullable<T>
            }

            return false; // value-type
        }

        /// <summary>
        /// MergeWith
        /// </summary>
        /// <param name="sourceEntity"></param>
        /// <param name="updatedEntity"></param>
        /// <param name="writeNull"></param>
        /// <param name="propertiesToNullify"></param>
        /// <param name="original">
        /// </param>
        /// <param name="updated">
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static TEntity MergeWith<TEntity>(this TEntity sourceEntity, TEntity updatedEntity, bool writeNull = false, string[] propertiesToNullify = null)
        {
            var properties = typeof(TEntity).GetRuntimeProperties();

            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

				MethodInfo castMethod = typeof(ObjectHelpers).GetMethod("ToType").MakeGenericMethod(prop.PropertyType);

				var updatedEntityValue = castMethod.Invoke(null, new [] { prop.GetValue(updatedEntity, null) }); ;
                var sourceEntityValue = castMethod.Invoke(null, new [] { prop.GetValue(sourceEntity, null) });
				
				if (!ObjectHelpers.ValueEquality(updatedEntityValue, sourceEntityValue) 
					&& !(updatedEntityValue is Guid && ObjectHelpers.ValueEquality((Guid)updatedEntityValue, default(Guid))))
				{
					if (updatedEntityValue != null || (writeNull) || (propertiesToNullify != null && propertiesToNullify.Contains(prop.Name)))
                        prop.SetValue(sourceEntity, updatedEntityValue, null);
                }
            }

            return sourceEntity;
        }
		
		/// <summary>
		/// The remove last separated element.
		/// </summary>
		/// <param name="separatedString">
		/// The separated string.
		/// </param>
		/// <param name="separator">
		/// The separator.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public static string RemoveLastSeparatedElement(this string separatedString, char separator)
        {
            string[] array = separatedString.Split(separator);

            if (array.Count() <= 1)
            {
                return string.Empty;
            }

            array[array.Count() - 1] = string.Empty;
            return string.Join(separator.ToString(), array).TrimEnd(new[] { '.' });
        }

        /// <summary>
        /// The to size string.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToSizeString(this double bytes)
        {
            CultureInfo culture = CultureInfo.CurrentUICulture;
            const string format = "#,0.0";

            if (bytes < 1024)
            {
                return bytes.ToString("#,0", culture);
            }

            bytes /= 1024;
            if (bytes < 1024)
            {
                return bytes.ToString(format, culture) + " KB";
            }

            bytes /= 1024;
            if (bytes < 1024)
            {
                return bytes.ToString(format, culture) + " MB";
            }

            bytes /= 1024;
            if (bytes < 1024)
            {
                return bytes.ToString(format, culture) + " GB";
            }

            bytes /= 1024;
            return bytes.ToString(format, culture) + " TB";
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get method ext.
        /// </summary>
        /// <param name="matchingMethod">
        /// The matching method.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterTypes">
        /// The parameter types.
        /// </param>
        /// <exception cref="AmbiguousMatchException">
        /// </exception>
        private static void GetMethodExt(
            ref MethodInfo matchingMethod, 
            Type type, 
            string name, 
            params Type[] parameterTypes)
        {
            // Check all methods with the specified name, including in base classes
            foreach (MethodInfo methodInfo in type.GetRuntimeMethods().Where(x => x.Name == name))
            {
                // Check that the parameter counts and types match, with 'loose' matching on generic parameters
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == parameterTypes.Length)
                {
                    int i = 0;
                    for (; i < parameterInfos.Length; ++i)
                    {
                        if (!parameterInfos[i].ParameterType.IsSimilarType(parameterTypes[i]))
                        {
                            break;
                        }
                    }

                    if (i == parameterInfos.Length)
                    {
                        if (matchingMethod == null)
                        {
                            matchingMethod = methodInfo;
                        }
                        else
                        {
                            throw new AmbiguousMatchException("More than one matching method found!");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The i list logic.
        /// </summary>
        /// <param name="OriginalEntity">
        /// The original entity.
        /// </param>
        /// <param name="CurrentProperty">
        /// The current property.
        /// </param>
        /// <param name="originalValue">
        /// The original value.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        private static void IListLogic<TEntity>(
            TEntity OriginalEntity, 
            PropertyInfo CurrentProperty, 
            object originalValue)
        {
            if (originalValue is IList)
            {
                var tempList = originalValue as IList;
                var existingList = CurrentProperty.GetValue(OriginalEntity) as IList;

                foreach (object item in tempList)
                {
                    existingList.Add(item);
                }
            }
        }

        /// <summary>
        /// Determines if the two types are either identical, or are both generic parameters or generic types
        ///     with generic parameters in the same locations (generic parameters match any other generic paramter,
        ///     but NOT concrete types).
        /// </summary>
        /// <param name="thisType">
        /// The this Type.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsSimilarType(this Type thisType, Type type)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
            {
                thisType = thisType.GetElementType();
            }

            if (type.IsByRef)
            {
                type = type.GetElementType();
            }

            // Handle array types
            if (thisType.IsArray && type.IsArray)
            {
                return thisType.GetElementType().IsSimilarType(type.GetElementType());
            }

            if (thisType.Name == "System.RuntimeType" && type.Name == "System.Type")
            {
                return true;
            }

            // If the types are identical, or they're both generic parameters or the special 'T' type, treat as a match
            if (thisType == type
                || ((thisType.IsGenericParameter || thisType == typeof(T))
                    && (type.IsGenericParameter || type == typeof(T))))
            {
                return true;
            }

            // Handle any generic arguments
            if (thisType.GetTypeInfo().IsGenericType && type.GetTypeInfo().IsGenericType)
            {
                Type[] thisArguments = thisType.GenericTypeArguments;
                Type[] arguments = type.GenericTypeArguments;
                if (thisArguments.Length == arguments.Length)
                {
                    for (int i = 0; i < thisArguments.Length; ++i)
                    {
                        if (!thisArguments[i].IsSimilarType(arguments[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        #endregion
            
        /// <summary>
        ///     Special type used to match any generic parameter type in GetMethodExt().
        /// </summary>
        public class T
        {

        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.Any(type1 => type1.Name == "IEnumerable");
        }

        public static string ToID(this string value)
        {
            return $"{value}ID";
        }

        public static IEnumerable<T> ApplyExplicitProperties<T>(this IEnumerable<T> data, IEnumerable<string> explicitProperties) where T : CoreObject
        {
            if (explicitProperties != null)
            {
                explicitProperties=explicitProperties.Concat(new[] { "Id", "Created" });
                var query = data.AsQueryable();
                query = query.Select(ReflectionHelpers.DynamicSelectQuery<T>(explicitProperties)).AsQueryable();
                return query.ToList().AsEnumerable();
            }

            return data;
        }

        public static IQueryable<T> ApplyExplicitProperties<T>(this IQueryable<T> data, IQueryable<string> explicitProperties) where T : CoreObject
        {
            if (explicitProperties != null)
            {
                explicitProperties=explicitProperties.Concat(new[] { "Id", "Created" });
                var query = data.AsQueryable();
                return query.Select(ReflectionHelpers.DynamicSelectQuery<T>(explicitProperties)).AsQueryable();
            }

            return data;
        }
    }
}