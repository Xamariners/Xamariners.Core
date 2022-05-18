// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PagedResults.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamariners.Core.Common.Helpers
{

    /// <summary>
    /// pagination
    /// </summary>
    public static class PaginationHelpers
    {

        /// <summary>
        /// The set pagination.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="dbset">
        /// The dbset.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The IEnumerable
        /// </returns>
		public static IQueryable<T> SetPagination<T>(IQueryable<T> dbset, int amount, int start, string order) where T : class
        {
            var query = dbset;
            if (!String.IsNullOrWhiteSpace(order))
                query = query.OrderByQueryable(order);

            if (start > 0)
            {
                if (String.IsNullOrWhiteSpace(order))
                    query = query.OrderByQueryable("Id");

                query = query.Skip(start);
            }

            if (amount > 0)
                query = query.Take(amount);

            return query;
        }

        /// <summary>
        /// The set pagination.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="dbset">
        /// The dbset.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The IEnumerable
        /// </returns>
        public static IEnumerable<T> SetPagination<T>(IEnumerable<T> dbset, int amount, int start, string order)
            where T : class
        {
            if (start != 0 && amount != 0 && !string.IsNullOrEmpty(order))
            {
                return dbset.OrderByEnumerable(order).Skip(start).Take(amount);
            }

            if (start != 0 && amount != 0)
            {
                return dbset.Skip(start).Take(amount);
            }

            if (start != 0 && !string.IsNullOrEmpty(order))
            {
                return dbset.OrderByEnumerable(order).Skip(start);
            }

            if (amount != 0 && !string.IsNullOrEmpty(order))
            {
                return dbset.OrderByEnumerable(order).Take(amount);
            }

            if (amount != 0)
            {
                return dbset.Take(amount);
            }

            if (start != 0)
            {
                return dbset.Skip(start);
            }

            if (!string.IsNullOrEmpty(order))
            {
                return dbset.OrderByEnumerable(order);
            }

            return dbset;
        }
    
    }
}