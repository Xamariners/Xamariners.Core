namespace Xamariners.Core.Interface
{
    public interface IDbEntry
    {
        EntityState State { get; set; }
        object Entity { get; set; }
        //IDbProperty Property<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> excludePropertyExpression);
    }
}