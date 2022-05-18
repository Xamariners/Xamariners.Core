// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeUnitOfWork.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Interface;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.FakeData
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public class FakeUnitOfWork : IUnitOfWork
    {
        #region Constants and Fields

        private readonly IDatabaseFactory databaseFactory;

        private List<Action> _actions { get; set; }

        /// <summary>
        /// The data context.
        /// </summary>

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeUnitOfWork"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public FakeUnitOfWork()
        {
            _actions = new List<Action>();
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

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeUnitOfWork"/> class.
        /// </summary>
        /// <param name="databaseFactory">
        /// The database factory.
        /// </param>
        public FakeUnitOfWork(IDatabaseFactory databaseFactory) : this()
        {
            this.databaseFactory = databaseFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The commit.
        /// </summary>
        public void Commit()
        {
            // do nothing for repository

            // execute actions
            foreach (var action in _actions)
                action();

            _actions.Clear();
        }

       

        public void AddAction(Action action)
        {
            _actions.Add(action);
        }

        public IContext Context { get; set; }

        #endregion
    }
}