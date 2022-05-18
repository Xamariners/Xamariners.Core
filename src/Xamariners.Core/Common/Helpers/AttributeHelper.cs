// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributeHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The attribute helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamariners.Core.Common.Helpers;


namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The attribute helper.
    /// </summary>
    public static class AttributeHelper
    {
        public static Dictionary<string, TAttribute> GetAttributes<TAttribute, TType>() where TAttribute : class 
        {
            Type type = typeof(TType);
            var result = new Dictionary<string, TAttribute>();
            var attributeType = typeof(TAttribute);
            var properties = type.GetRuntimeProperties();

            foreach (var memberInfo in properties)
            {
                var attributes = memberInfo.GetCustomAttributes(attributeType, false);
                foreach (var attribute in attributes)
                {
                    if (attribute is TAttribute)
                    result.Add(memberInfo.Name, attribute as TAttribute);
                }
                    
            }

            return result;
        }

        public static TAttribute GetAttribute<TAttribute, TType>() where TAttribute : Attribute
        {
            return GetAttributes<TAttribute, TType>().FirstOrDefault().Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <typeparam name="TClass"></typeparam>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static TAttribute GetAttributeForMethod<TClass, TAttribute>(string methodName) where TAttribute : class
        {
            Type attributeType = typeof(TAttribute);
            var attribute = typeof(TClass).GetMethod(methodName).GetType().GetCustomAttributes(attributeType, false).SingleOrDefault() as TAttribute;
            return attribute;
        }
    }
}