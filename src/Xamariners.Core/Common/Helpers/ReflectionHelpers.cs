// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelpers.cs" company="">
//   
// </copyright>
// <summary>
//   the reflection helpers
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PCLAppConfig;
using Xamariners.Core.Configuration;
using Xamariners.Core.Interface;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     the reflection helpers
    /// </summary>
    public static class ReflectionHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// ExecuteGenericMethod
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <param name="type">
        /// </param>
        /// <param name="genericType">
        /// </param>
        /// <param name="methodName">
        /// </param>
        /// <param name="args">
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object ExecuteGenericMethod(
            object obj,
            Type type,
            Type genericType,
            string methodName,
            params object[] args)
        {
            try
            {
                MethodInfo method = null;

                try
                {
                    method = type.GetRuntimeMethods().SingleOrDefault(x => x.Name == methodName);
                }
                finally
                {
                    if (args != null && method == null)
                    {
                        Type[] types = args.Select(o => o == null ? typeof(object) : o.GetType()).ToArray();
                        method = type.GetMethodExt(methodName, types);
                    }
                }

                if (method == null)
                    throw new Exception(String.Format("Can't find method '{0}' on type '{1}'", methodName, type.FullName));

                MethodInfo generic = method.MakeGenericMethod(genericType);
                return generic.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }

                throw;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodExpression"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetMethodName<T>(Expression<Action<T>> methodExpression)
        {
            return ((MethodCallExpression)methodExpression.Body).Method.Name;
        }

        public static object ExecuteMethodOnInstance<T>(object instance, Expression<Action<T>> methodExpression)
        {
            string methodName = GetMethodName(methodExpression);
            var argsValue =
                (((methodExpression.Body as MethodCallExpression).Arguments[0] as MemberExpression).Expression as
                    ConstantExpression).Value;

            var members = (methodExpression.Body as MethodCallExpression).Arguments.Select(
                expression => (expression as MemberExpression).Member);

            object[] args = members.Select(info => argsValue.ReadField(info.Name)).ToArray();

            //Type[] argsTypes = args.Select(x => x.GetType()).ToArray();

            var method = instance.GetType().GetMethod(methodName);

            if (method == null)
                throw new Exception($"Cannot find method {methodName} in type {instance.GetType()} with {args.Count()} arguments");

            var result = method.Invoke(instance, args);
            return result;
        }

        public static Type GetImplementor<TInterface>() where TInterface : class
        {
            return GetImplementor(typeof(TInterface));
        }

        public static Type GetImplementor(Type interfaceType)
        {
            var coreAssemblyTypes = Assembly.Load(new AssemblyName("Xamariners.Core")).DefinedTypes;

            var coreTypes = coreAssemblyTypes.Where(
                    type => interfaceType.GetTypeInfo().IsAssignableFrom(type) &&
                            type.Namespace == "Xamariners.Core.Service.Interface" && type.IsInterface);

            var serviceAssemblyName = ConfigurationManager.AppSettings["service.assemblyname"];
            var serviceNamespace = ConfigurationManager.AppSettings["service.namespace"];

            var types = Assembly.Load(new AssemblyName(serviceAssemblyName)).DefinedTypes
                .Where(
                    type => interfaceType.GetTypeInfo().IsAssignableFrom(type)
                            && type.Namespace == serviceNamespace && type.IsInterface
                );

            types = types.Union(coreTypes);

            if (!types.Any())
                throw new Exception($"No type implements interface '{interfaceType.Name}'");

            try
            {
                return types.Single().AsType();
            }
            catch (Exception exception)
            {
                throw new Exception($"Too many types implement interface '{interfaceType.Name}'",
                    exception);
            }
        }

        public static bool AreChildNotNull(object parent)
        {
            return
                parent.GetType().GetProperties()
                    .Select(pi => pi.GetValue(parent))
                    .Any(value => value != null);
        }

        public static Func<T, T> DynamicSelectQuery<T>(IEnumerable<string> fields)
        {
            // input parameter "x"
            var xParameter = Expression.Parameter(typeof(T), "x");

            // new statement "new Data()"
            var xNew = Expression.New(typeof(T));
                // create initializers
                var bindings = fields
                    .Select(x =>
                    {
                        // property "Field1"
                        var mi = typeof(T).GetProperty(x);
                        if(mi==null)
                            throw new Exception($"Column Name is Invalid '{x}'");
                        // original value "x.Field1"
                        var xOriginal = Expression.Property(xParameter, mi);
                        // set value "Field1 = x.Field1"
                        return Expression.Bind(mi, xOriginal);
                    }
                    );

                // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
                var xInit = Expression.MemberInit(xNew, bindings);

                // expression "x => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
                var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);

                // compile to Func<Data, Data>
                return lambda.Compile();
        }

        public static object RemoveObjectsByNameAndIdRecursive(object instance, string propertyName, object id, int level = 4)
        {

            if (!(instance is CoreObject || instance is ICollection<CoreObject>) || level == 0)
                return instance; 

            if (propertyName.Contains("."))
                propertyName = propertyName.Split('.').ToList().Last();

            var props = instance.GetType().GetProperties(ReflectionExtensions.BindingFlags.DeclaredOnly | ReflectionExtensions.BindingFlags.Public | ReflectionExtensions.BindingFlags.Instance)              
                .ToList();

            foreach (PropertyInfo prop in props)
            {
                try
                {
                    object propValue = prop.GetValue(instance, null);

                    if (propValue == null)
                        continue;

                    if (prop.PropertyType.IsEnumerable() && prop.PropertyType.GetTypeInfo().IsGenericType
                    && prop.PropertyType.GetGenericArguments()[0].GetTypeInfo().IsSubclassOf(typeof(CoreObject)))
                    {
                        var collection = (IList)propValue;

                        for (var i = collection.Count - 1; i >= 0; --i)
                            if (RemoveObjectsByNameAndIdRecursive(collection[i], propertyName, id, level - 1) == null)
                                collection.RemoveAt(i);
                    }
                    else
                    {
                        // nothing else to recurse
                        if (propValue == null)
                            continue;

                        if (prop.Name == propertyName && !propValue.Equals(id))
                        {
                            // indicates remove parent value
                            return null;
                        }

                        var result = RemoveObjectsByNameAndIdRecursive(propValue, propertyName, id, level - 1);

                        if (result == null)
                            prop.SetValue(instance, null);
                    }

                }
                catch (Exception ex)
                {
                    ;
                }
            }

            // end
            return instance;
        }
    }
}