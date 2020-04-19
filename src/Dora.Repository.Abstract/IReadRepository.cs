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
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetAsync(TPrimaryKey id);
  
        Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);
    }
}
