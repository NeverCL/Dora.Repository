using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class EfCoreRepository<TDbContext, TEntity, TPrimaryKey> :
        RepositoryBase<TEntity, TPrimaryKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TPrimaryKey>
    {
        DbContext dbContext;
        public EfCoreRepository(IDbContextProvider dbContextProvider)
        {
            dbContext = dbContextProvider.GetDbContext();
        }

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public override TEntity Insert(TEntity entity)
        {
            dbContext.Add(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}
