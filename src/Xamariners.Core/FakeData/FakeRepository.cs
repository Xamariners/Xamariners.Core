// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeRepositoryBase.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MoreLinq;
using Xamariners.Core.Data;
using Xamariners.Core.Interface;

namespace Xamariners.Core.FakeData
{
  

    using Xamariners.Core.Common.Helpers;

    /// <summary>
    /// The fake repository base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class FakeRepository : RepositoryBase
    {
        private readonly IContext _context;

        public FakeRepository(IDatabaseFactory databaseFactory, IContext context) : base(databaseFactory)
        {
            _context = context;
        }

        public override IDbSet<T> GetDbSet<T>()
        {
            return _context.GetDbSet<T>();
        }

        public override void Add<T>(T entity, IContext context = null)
        {
            T existing = GetById<T>(entity.Id);
            if (existing != null)
            {
                this.Delete<T>(entity.Id);
            }

            this.GetDbSet<T>().Add(entity);
        }

        public override void Update<T>(Guid id, T updatedEntity, IContext context = null, string[] propertiesToNullify = null, bool bypassMerge = false)
        {
            var sourceEntity = GetDbSet<T>().FirstOrDefault(x => x.Id == id);

            if (sourceEntity != null)
                sourceEntity = sourceEntity.MergeWith(updatedEntity, propertiesToNullify: propertiesToNullify);

            sourceEntity.Updated = DateTime.UtcNow;

            if (sourceEntity != null)
                this.Delete(sourceEntity);

            this.Add(sourceEntity);
        }

        public override T Delete<T>(T entity, IContext context = null)
        {
            if (entity != null)
            {
                if (context != null)
                {
                    var dbSet = context.GetDbSet<T>();
                    entity = dbSet.Remove(entity);
                }
                else
                {
                    context = GetNewContext();
                    var dbSet = context.GetDbSet<T>();
                    entity = dbSet.Remove(entity);
                }
            }
            
            return entity;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse{T}"/>.
        /// </returns>
        public override T Delete<T>(Guid id, IContext IContext = null)
        {
            var entity = this.GetById<T>(id);

            if (entity != null)
            {
                if (IContext is IContext)
                {
                    var dbSet = (IContext as IContext).GetDbSet<T>();
                    entity = dbSet.Remove(entity);
                }
                else
                {
                    var context = GetNewContext();
                    var dbSet = context.GetDbSet<T>();
                    entity = dbSet.Remove(entity);                 
                }
            }
           
            return entity;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public override T Get<T>(Expression<Func<T, bool>> where, string order = "", IContext context = null)
        {
            context = context ?? GetNewContext();

            // break refs
            var dbset = GetNewContext().GetDbSet<T>().ToList();
            var dbsetClone = dbset.JsonClone()?.AsQueryable();
            var queryResult = dbsetClone.Where(where);

            //todo: fake returns duplicates somethi
            var result = PaginationHelpers.SetPagination(queryResult, 0, 0, order).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// The get all.
        /// </summary>
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
        /// The <see cref="List"/>.
        /// </returns>
        public override IEnumerable<T> GetAll<T>(int amount = 0, int start = 0, string order = "")
        {
            // break refs
            var dbset = GetNewContext().GetDbSet<T>().ToList();
            var dbsetClone = dbset.JsonClone().AsQueryable();

            // break refs
            var result = PaginationHelpers.SetPagination(dbsetClone, amount, start, order).ToList();
            return result;
        }

        /// <summary>
        /// The get many.
        /// </summary>
        /// <param name="where">
        /// The where.
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
        /// The <see cref="List"/>.
        /// </returns>
        public override IEnumerable<T> GetMany<T>(Expression<Func<T, bool>> where, int amount = 0, int start = 0, string order = "")
        {
            // break refs
            var dbset = GetNewContext().GetDbSet<T>().ToList();
            var dbsetClone = dbset.JsonClone().AsQueryable();
            var queryResult = dbsetClone.Where(@where).AsEnumerable().DistinctBy(x => x.Id);
           
            var result = PaginationHelpers.SetPagination(queryResult, amount, start, order);
            
            return result;
        }
    }
}