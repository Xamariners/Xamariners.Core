// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Interface
{
    /// <summary>
    /// The i repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IRepository : IUnitOfWork
    {
        #region Public Properties

        /// <summary>
        /// DB set
        /// </summary>
        IDbSet<T> GetDbSet<T>() where T : CoreObject;
        
        #endregion


        #region Public Methods and Operators

        void Add<T>(T entity, IContext context = null) where T : CoreObject;

        T Delete<T>(Guid id, IContext context = null) where T : CoreObject;

        T Delete<T>(T entity, IContext context = null) where T : CoreObject;

        int Delete<T>(Expression<Func<T, bool>> where) where T : CoreObject;


        T Get<T>(Expression<Func<T, bool>> where, string order = "", IContext xamarinersContext = null) where T : CoreObject;

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="amount">
        /// </param>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        IEnumerable<T> GetAll<T>(int amount = 0, int start = 0, string order = "") where T : CoreObject;

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="Id">
        /// The id.
        /// </param>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        T GetById<T>(Guid Id) where T : CoreObject;

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        long Count<T>(Expression<Func<T, bool>> where = null) where T : CoreObject;

        void Update<TEntity>(Guid id, TEntity entity, IContext context = null, string[] propertiesToNullify = null, bool bypassMerge = false) where TEntity : CoreObject;
        #endregion

        void Update<TEntity, TProperty>(Guid id, TEntity entity, IContext context = null,
            Expression<Func<CoreObject, TProperty>> excludePropertyExpression = null) where TEntity : CoreObject;

        IEnumerable<T> GetMany<T>(IQueryable<T> query, int amount = 0, int start = 0, string order = null) where T : CoreObject;
        IEnumerable<T> GetMany<T>(Expression<Func<T, bool>> query, int amount = 0, int start = 0, string order = null) where T : CoreObject;

        IUnitOfWork GetUnitOfWork();
    }
}