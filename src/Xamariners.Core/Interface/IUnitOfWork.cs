using System;
using System.Collections.Generic;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Interface
{
    /// <summary>
    /// The i unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Public Methods

        /// <summary>
        /// The commit.
        /// </summary>
        void Commit();

        /// <summary>
        /// The actions that will be executed on commit.
        /// </summary>
        void AddAction(Action action);

        void DetachEntity<T>(CoreObject parentEntity, T childEntity, string childEntityName = null) where T : CoreObject;

        void DetachEntities<T>(CoreObject parentEntity, T childEntities, string childEntityName)
            where T : IEnumerable<CoreObject>;
        
        IContext Context { get; set; }

        #endregion
    }
}