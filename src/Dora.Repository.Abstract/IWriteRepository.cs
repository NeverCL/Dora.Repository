using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Repository.Abstract
{
    public interface IWriteRepository<TEntity,TPrimaryKey> : IRepository
    {
        #region Insert
        // /// <summary>
        // /// Inserts a new entity.
        // /// </summary>
        // /// <param name="entity">Inserted entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        // Task InsertAsync(TEntity entity);
        // Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        // /// <summary>
        // /// Inserts or updates given entity depending on Id's value.
        // /// </summary>
        // /// <param name="entity">Entity</param>
        // TEntity InsertOrUpdate(TEntity entity);

        // /// <summary>
        // /// Inserts or updates given entity depending on Id's value.
        // /// </summary>
        // /// <param name="entity">Entity</param>
        // Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        #endregion

        #region Update

        // /// <summary>
        // /// Updates an existing entity.
        // /// </summary>
        // /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        // Task UpdateAsync(TEntity entity);
        // Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        // /// <summary>
        // /// Updates an existing entity.
        // /// </summary>
        // /// <param name="id">Id of the entity</param>
        // /// <param name="updateAction">Action that can be used to change values of the entity</param>
        // /// <returns>Updated entity</returns>
        // TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        // /// <summary>
        // /// Updates an existing entity.
        // /// </summary>
        // /// <param name="id">Id of the entity</param>
        // /// <param name="updateAction">Action that can be used to change values of the entity</param>
        // /// <returns>Updated entity</returns>
        // Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion

        #region Delete

        // /// <summary>
        // /// Deletes an entity by primary key.
        // /// </summary>
        // /// <param name="id">Primary key of the entity</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        // Task DeleteAsync(TPrimaryKey id);
        // Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

        // /// <summary>
        // /// Deletes many entities by function.
        // /// Notice that: All entities fits to given predicate are retrieved and deleted.
        // /// This may cause major performance problems if there are too many entities with
        // /// given predicate.
        // /// </summary>
        // /// <param name="predicate">A condition to filter entities</param>
        // void Delete(Expression<Func<TEntity, bool>> predicate);

        // /// <summary>
        // /// Deletes many entities by function.
        // /// Notice that: All entities fits to given predicate are retrieved and deleted.
        // /// This may cause major performance problems if there are too many entities with
        // /// given predicate.
        // /// </summary>
        // /// <param name="predicate">A condition to filter entities</param>
        // Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
