using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Repository.Abstract
{
    public interface IReadRepository<TEntity, TPrimaryKey>: IRepository
    {
        #region Select/Get/Query

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// One or more 
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns>IQueryable to be used to select entities from database</returns>
        // IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        // /// <summary>
        // /// Gets an entity with given primary key.
        // /// </summary>
        // /// <param name="id">Primary key of the entity to get</param>
        // /// <returns>Entity</returns>
        // TEntity Get(TPrimaryKey id);

        Task<TEntity> GetAsync(TPrimaryKey id);
        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

        #endregion
    }
}
