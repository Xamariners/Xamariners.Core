// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CommonServiceLocator;
using Xamariners.Core.Common;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Interface;
using Xamariners.Core.Model.Internal;
using Xamariners.RestClient.Models;

namespace Xamariners.Core.Data
{
    /// <summary>
    /// The repository base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class RepositoryBase : IRepository
    {
        #region Fields

        /// <summary>
        ///     The dbset.
        /// </summary>
        public virtual IDbSet<T> GetDbSet<T>() where T : CoreObject
        {
            return Context.GetDbSet<T>();
        }

        private IDatabaseFactory _databaseFactory;
        private List<Action> _actions { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public RepositoryBase(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _actions = new List<Action>();
        }

        #endregion

        #region Public Properties


        public IContext Context
        {
            get { return _databaseFactory.GetContext() as IContext; }
            set { throw new NotImplementedException(); }
        }

        protected IContext GetNewContext()
        {
            return _databaseFactory.CreateNewContext() as IContext;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            unitOfWork.Context = Context;
            return unitOfWork;
        }
        
        #endregion

        #region Public Methods and Operators

      
        public virtual T Delete<T>(T entity, IContext context = null) where T : CoreObject 
        {
            if (entity != null)
            {
                if (context != null)
                {
                    var dbSet = context.GetDbSet<T>();
                    dbSet.Attach(entity);
                    entity = dbSet.Remove(entity);
                }
                else
                {
                    context = GetNewContext();
                    var dbSet = context.GetDbSet<T>();
                    dbSet.Attach(entity);
                    entity = dbSet.Remove(entity);
                    context.Commit();
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
        public virtual T Delete<T>(Guid id, IContext IContext = null) where T : CoreObject 
        {
            var entity = this.GetById<T>(id);
            
            if (entity != null)
            {
                if (IContext is IContext)
                {
                    var dbSet = (IContext as IContext).GetDbSet<T>();
                    dbSet.Attach(entity);
                    entity = dbSet.Remove(entity);
                }
                else
                {
                    var context = GetNewContext();
                    var dbSet = context.GetDbSet<T>();
                    dbSet.Attach(entity);
                    entity = dbSet.Remove(entity);
                    context.Commit();
                }
            }

            return entity;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse"/>.
        /// </returns>
        public int Delete<T>(Expression<Func<T, bool>> where) where T : CoreObject
        {
            var context = GetNewContext();
            var dbSet = context.GetDbSet<T>();
            var count = 0;
            var objects = dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                dbSet.Remove(obj);
                count++;
            }
            context.Commit();

            return count;
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
        public virtual T Get<T>(Expression<Func<T, bool>> where, string order = "", IContext context = null) where T : CoreObject
        {
            context = context ?? GetNewContext();
            var dbset = context.GetDbSet<T>();
            return PaginationHelpers.SetPagination(dbset.Where(where).AsQueryable(), 0, 0, order).SingleOrDefault();
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
        public virtual IEnumerable<T> GetAll<T>(int amount = 0, int start = 0, string order = "") where T : CoreObject 
        {
            return PaginationHelpers.SetPagination(GetNewContext().GetDbSet<T>().AsQueryable(), amount, start, order).ToList();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById<T>(Guid id) where T : CoreObject
        {
            return Get<T>(x => x.Id == id);
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
        public virtual IEnumerable<T> GetMany<T>(Expression<Func<T, bool>> where, int amount = 0, int start = 0, string order = "") where T : CoreObject 
        {
            return PaginationHelpers.SetPagination(GetNewContext().GetDbSet<T>().Where(where), amount, start, order);
        }
       

        public virtual IEnumerable<T> GetMany<T>(IQueryable<T> query, int amount, int start, string order) where T : CoreObject
        {
            return PaginationHelpers.SetPagination(query, amount, start, order);
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long Count<T>(Expression<Func<T, bool>> where = null) where T : CoreObject
        {
            if (where == null)
            {
                return GetNewContext().GetDbSet<T>().Count();
            }

            return GetNewContext().GetDbSet<T>().Count(@where);
        }

        public virtual void Add<T>(T entity, IContext context = null) where T : CoreObject
        {
            if (entity != null)
            {
                entity.Updated = entity.Created = DateTime.UtcNow;

                if (context != null)
                {
                    var dbSet = context.GetDbSet<T>();
                    dbSet.Add(entity);
                    context.ManageObjectRelationship(entity);
                }
                else
                {
                    context = GetNewContext();
                    var dbSet = context.GetDbSet<T>();
                    dbSet.Add(entity);
                    context.ManageObjectRelationship(entity);
                    context.Commit();
                }
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="xamarinersContext"></param>
        /// <param name="excludePropertyExpression"></param>
        /// <param name="propertiesToNullify"></param>
        /// <returns>
        /// The <see cref="ServiceResponse"/>.
        /// </returns>
        /// ynamic
        public virtual void Update<T>(Guid id, T updatedEntity, IContext context = null, string[] propertiesToNullify = null, bool bypassMerge = false) where T : CoreObject
        {
            bool isTransaction = context != null;
            T sourceEntity = null;

            if (updatedEntity != null)
            {
                updatedEntity.Updated = DateTime.UtcNow;

                if(context == null)
                    context = GetNewContext();

                try
                {
                    if (!isTransaction)
                    {
                        // not a transaction. we got a fresh context, so the remote data is accurate 
                        sourceEntity = GetById<T>(id);
                    }
                    else
                    {
                        // in transaction. remote data is potentiatally stale (as not updated from operations in stransaction). 
                       
                        // bypass merge
                        if (bypassMerge)
                        {
                            context.ModifyEntryState(updatedEntity);
                            context.ManageObjectRelationship(updatedEntity);
                            return;
                        }
                        
                        // check if this object is in the tracker , otherwise get the remote db version
                        sourceEntity = context.GetModifiedEntity<T>(updatedEntity.Id) ?? GetById<T>(id);
                    }

                    if (sourceEntity == null)
                        throw new Exception($"cannot update entity of type {typeof(T).Name} and id {id} : not found");

                    sourceEntity = sourceEntity.MergeWith(updatedEntity, false, propertiesToNullify);

                    context.ModifyEntryState(sourceEntity);

                    context.ManageObjectRelationship(updatedEntity);
                }
                catch (InvalidOperationException exception)
                {
                    //HACK: if we update the same object twice, re-attaching the entity fails
                    if (!exception.Message.Contains("another entity of the same type already has the same primary key value"))
                        throw;
                }

                if (!isTransaction)
                    context.Commit();
            }
        }

        public void Update<TEntity, TProperty>(Guid id, TEntity entity, IContext context = null, Expression<Func<CoreObject, TProperty>> excludePropertyExpression = null) where TEntity : CoreObject
        {
            if (entity != null)
            {
                bool isTransaction = context != null;

                entity.Id = id;
                entity.Updated = DateTime.UtcNow;

                if (context == null)
                    context = GetNewContext();

                context.ModifyEntryState(entity);

                if (excludePropertyExpression != null)
                    context.ExcludeProperty(entity, excludePropertyExpression);

                if (!isTransaction)
                    context.Commit();
            }
        }


        ///// <summary>
        ///// The update.
        ///// </summary>
        ///// <param name="id">
        /////     The id.
        ///// </param>
        ///// <param name="entity">
        /////     The entity.
        ///// </param>
        ///// <param name="IContext"></param>
        ///// <param name="excludePropertyExpression"></param>
        ///// <returns>
        ///// The <see cref="ServiceResponse"/>.
        ///// </returns>
        ///// ynamic
        //public virtual void Update<T>(Guid id, T entity, IContext context = null) where T : CoreObject
        //{
        //    if (entity != null)
        //    {
        //        entity.Updated = DateTime.UtcNow;
        //        bool isSharedContext = context != null;
        //        context = context ?? GetNewContext();

        //        try
        //        {
        //            context.Entry(entity).State = EntityState.Modified;
        //        }
        //        catch (InvalidOperationException exception)
        //        {
        //            //HACK: if we update the same object twice, re-attaching the entity fails
        //            if (!exception.Message.Contains("another entity of the same type already has the same primary key value"))
        //                throw;
        //        }

        //        if (!isSharedContext)
        //            context.Commit();
        //    }
        //}

        //public void Update<TEntity, TProperty>(Guid id, TEntity entity, IContext context = null, Expression<Func<TEntity, TProperty>> excludePropertyExpression = null) where TEntity : CoreObject
        //{
        //    if (entity != null)
        //    {
        //        entity.Id = id;
        //        entity.Updated = DateTime.UtcNow;
        //        bool isSharedContext = context != null;
        //        context = context ?? GetNewContext();
        //        context.Entry(entity).State = EntityState.Modified;

        //        //if (excludePropertyExpression != null)
        //        //    context.Entry(entity).Property(excludePropertyExpression).IsModified = false;

        //        if (!isSharedContext)
        //            context.Commit();
        //    }
        //}

        #endregion

        public void Commit()
        {
            Context.Commit();
        }

        public void AddAction(Action action)
        {
            _actions.Add(action);
        }

        public void DetachEntity<T>(CoreObject parentEntity, T childEntity, string childEntityName = null) where T : CoreObject
        {
            // remove entity from parent, and readd after commit is done
            if (childEntityName == null)
                childEntityName = typeof(T).Name;

            parentEntity.GetType().GetProperty(childEntityName).SetValue(parentEntity, null);
            AddAction(() => parentEntity.GetType().GetProperty(childEntityName).SetValue(parentEntity, childEntity));
        }

        public void DetachEntities<T>(CoreObject parentEntity, T childEntities, string childEntitiesName) where T : IEnumerable<CoreObject>
        {
            parentEntity.GetType().GetProperty(childEntitiesName).SetValue(parentEntity, null);
            AddAction(() => parentEntity.GetType().GetProperty(childEntitiesName).SetValue(parentEntity, childEntities));
        }
    }
}