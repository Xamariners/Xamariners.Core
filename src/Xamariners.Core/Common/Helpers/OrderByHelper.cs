// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByHelper.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The order by helper.
    /// list.OrderBy("SomeProperty");
    /// list.OrderBy("SomeProperty DESC");
    /// list.OrderBy("SomeProperty DESC, SomeOtherProperty");
    /// list.OrderBy("SomeSubObject.SomeProperty ASC, SomeOtherProperty DESC");
    /// </summary>
    public static class OrderByHelper
    {
        #region Enums

        /// <summary>
        /// The sort direction.
        /// </summary>
        private enum SortDirection
        {
            /// <summary>
            /// The ascending.
            /// </summary>
            Ascending = 0, 

            /// <summary>
            /// The descending.
            /// </summary>
            Descending = 1
        }

		#endregion

        #region Public Methods and Operators

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The IEnumerable
        /// </returns>
		public static IEnumerable<T> OrderByEnumerable<T>(this IEnumerable<T> enumerable, string orderBy) where T : class
        {
            var q = enumerable.AsQueryable();

			return q.OrderBy<T>(orderBy);
        }

        public static IQueryable<T> OrderByQueryable<T>(this IQueryable<T> queryable, string orderBy)
            where T : class
        {
            return queryable.OrderBy(orderBy);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The apply order by.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        


		public static IEnumerable<MethodInfo> GetExtensionMethods(Assembly assembly, 
			string name)
		{
			var query = from type in assembly.DefinedTypes
				                    where type.IsSealed && !type.IsGenericType && !type.IsNested
			                    from method in type.DeclaredMethods
				                    where method.IsDefined(typeof(ExtensionAttribute), false)
				            	where method.Name == name
				            where method.GetGenericArguments().Length == 2
				            where method.GetParameters().Length == 2
			                    select method;
			return query;
		}

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByValues) where TEntity : class
        {
            IQueryable<TEntity> returnValue = null;

            if (String.IsNullOrEmpty(orderByValues))
            {
                return source;
            }

            string orderPair = orderByValues.Trim().Split(',')[0];
            string command = orderPair.ToUpper().Contains("DESC") ? "OrderByDescending" : "OrderBy";

            Type type = typeof(TEntity);
            ParameterExpression parameter = Expression.Parameter(type, "p");

            // in case we get crap type names from api
            string propertyName = orderPair.Split(' ')[0].Trim().UppercaseFirst();

            PropertyInfo property;
            MemberExpression propertyAccess;

            if (propertyName.Contains("."))
            {
                try
                {
                    // support to be sorted on child fields. 
                    string[] childProperties = propertyName.Split('.');
                    property = typeof(TEntity).GetRuntimeProperty(childProperties[0]);
                    propertyAccess = Expression.MakeMemberAccess(parameter, property);

                    for (int i = 1; i < childProperties.Length; i++)
                    {
                        Type t = property.PropertyType;
                        if (!t.IsConstructedGenericType)
                        {
                            property = t.GetRuntimeProperty(childProperties[i]);
                        }
                        else
                        {
                            property = t.GenericTypeArguments.First().GetRuntimeProperty(childProperties[i]);
                        }

                        propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                property = type.GetRuntimeProperty(propertyName);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, parameter);

            MethodCallExpression resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            returnValue = source.Provider.CreateQuery<TEntity>(resultExpression);

            if (orderByValues.Trim().Split(',').Count() > 1)
            {
                // remove first item
                string newSearchForWords = orderByValues.Remove(0, orderByValues.IndexOf(',') + 1);
                return source.OrderBy(newSearchForWords);
            }

            return returnValue;
        }

        /// <summary>
        /// The order by ex.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="ordering">
        /// The ordering.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<T> OrderByEx<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            Type type = typeof(T);
            PropertyInfo property = null;

            if (ordering.Contains("."))
            {
                // support to be sorted on child fields. 
                string[] childProperties = ordering.Split('.');
                property = typeof(T).GetRuntimeProperty(childProperties[0]);
            }
            else
            {
                property = type.GetRuntimeProperty(ordering);
            }

            ParameterExpression parameter = Expression.Parameter(type, "p");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
            LambdaExpression orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// The parse order by.
        /// </summary>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <returns>
        /// The IEnumerable
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (String.IsNullOrEmpty(orderBy))
            {
                yield break;
            }

            string[] items = orderBy.Split(',');
            bool initial = true;
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                {
                    throw new ArgumentException(
                        String.Format(
                            "Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", 
                            item));
                }

                string prop = pair[0].Trim();

                if (String.IsNullOrEmpty(prop))
                {
                    throw new ArgumentException(
                        "Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");
                }

                var dir = SortDirection.Ascending;

                if (pair.Length == 2)
                {
                    dir = "desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase)
                               ? SortDirection.Descending
                               : SortDirection.Ascending;
                }

                yield return new OrderByInfo { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }
        }


        #endregion

        /// <summary>
        /// The order by info.
        /// </summary>
        private class OrderByInfo
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the direction.
            /// </summary>
            public SortDirection Direction { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether initial.
            /// </summary>
            public bool Initial { get; set; }

            /// <summary>
            /// Gets or sets the property name.
            /// </summary>
            public string PropertyName { get; set; }

            #endregion
        }

        /// <summary>
        /// The OrderByExpression interface.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        public interface IOrderByExpression<TEntity> where TEntity : class
		{
            /// <summary>
            /// The apply order by.
            /// </summary>
            /// <param name="query">
            /// The query.
            /// </param>
            /// <returns>
            /// The <see cref="IOrderedQueryable"/>.
            /// </returns>
            IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query);

            /// <summary>
            /// The apply then by.
            /// </summary>
            /// <param name="query">
            /// The query.
            /// </param>
            /// <returns>
            /// The <see cref="IOrderedQueryable"/>.
            /// </returns>
            IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query);
		}

        /// <summary>
        /// The order by expression.
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TOrderBy">
        /// </typeparam>
        public class OrderByExpression<TEntity, TOrderBy> : IOrderByExpression<TEntity>
            where TEntity : class
		{
            /// <summary>
            /// The _expression.
            /// </summary>
            private Expression<Func<TEntity, TOrderBy>> _expression;

            /// <summary>
            /// The _descending.
            /// </summary>
            private bool _descending;

            /// <summary>
            /// Initializes a new instance of the <see cref="OrderByExpression{TEntity,TOrderBy}"/> class.
            /// </summary>
            /// <param name="expression">
            /// The expression.
            /// </param>
            /// <param name="descending">
            /// The descending.
            /// </param>
            public OrderByExpression(Expression<Func<TEntity, TOrderBy>> expression, 
				bool descending = false)
			{
				this._expression = expression;
				this._descending = descending;
			}

            /// <summary>
            /// The apply order by.
            /// </summary>
            /// <param name="query">
            /// The query.
            /// </param>
            /// <returns>
            /// The <see cref="IOrderedQueryable"/>.
            /// </returns>
            public IOrderedQueryable<TEntity> ApplyOrderBy(
				IQueryable<TEntity> query)
			{
				if (this._descending)
					return query.OrderByDescending(this._expression);
				else
					return query.OrderBy(this._expression);
			}

            /// <summary>
            /// The apply then by.
            /// </summary>
            /// <param name="query">
            /// The query.
            /// </param>
            /// <returns>
            /// The <see cref="IOrderedQueryable"/>.
            /// </returns>
            public IOrderedQueryable<TEntity> ApplyThenBy(
				IOrderedQueryable<TEntity> query)
			{
				if (this._descending)
					return query.ThenByDescending(this._expression);
				else
					return query.ThenBy(this._expression);
			}
		}
    }
}
