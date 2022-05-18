using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Interface
{
    public interface IContext : IDisposable
    {
        IDbEntry Entry(CoreObject entity);

        IDbSet<T> GetDbSet<T>() where T : CoreObject;

        void Commit();

        void ModifyEntryState(CoreObject entity);

        void ExcludeProperty<TProperty>(CoreObject entity, Expression<Func<CoreObject, TProperty>> excludePropertyExpression);

        void ManageObjectRelationship<TEntity>(TEntity entity) where TEntity : CoreObject;

        void CleanManyToManyChildCollection<TEntity>(TEntity entity, string collectionName) where TEntity : CoreObject;

        T GetModifiedEntity<T>(Guid id) where T : CoreObject;

        IQueryable<T> AsNoTracking<T>(IQueryable<T> source) where T : class;
    }
}