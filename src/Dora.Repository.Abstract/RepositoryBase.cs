using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Repository.Abstract
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
            where TEntity : IEntity<TPrimaryKey>
    {
        public abstract void Delete(TPrimaryKey id);

        public abstract IQueryable<TEntity> GetAll();

        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
               => GetAsync(id, CancellationToken.None);

        public virtual Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            return Task.FromResult(GetAll().FirstOrDefault(CreateEqualityExpressionForId(id)));
        }

        public abstract void Insert(TEntity entity);

        public abstract void Update(TEntity entity);

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
    // public abstract class AsyncRepositoryBase<TEntity, TPrimaryKey> : IAsyncRepository<TEntity, TPrimaryKey>
    //         where TEntity : IEntity<TPrimaryKey>
    // {
    //     public abstract Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

    //     public virtual Task DeleteAsync(TPrimaryKey id)
    //         => DeleteAsync(id, CancellationToken.None);

    //     public abstract IQueryable<TEntity> GetAll();

    //     public virtual Task<TEntity> GetAsync(TPrimaryKey id)
    //         => GetAsync(id, CancellationToken.None);

    //     public virtual Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult( 
    //             GetAll().FirstOrDefault(CreateEqualityExpressionForId(id))
    //         );
    //     }

    //     public virtual Task InsertAsync(TEntity entity)
    //         => InsertAsync(entity, CancellationToken.None);
    //     public abstract Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

    //     public virtual Task UpdateAsync(TEntity entity)
    //         => UpdateAsync(entity, CancellationToken.None);
    //     public abstract Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    //     protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    //     {
    //         var lambdaParam = Expression.Parameter(typeof(TEntity));

    //         var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

    //         var idValue = Convert.ChangeType(id, typeof(TPrimaryKey));

    //         Expression<Func<object>> closure = () => idValue;
    //         var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

    //         var lambdaBody = Expression.Equal(leftExpression, rightExpression);

    //         return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    //     }
    // }
}
