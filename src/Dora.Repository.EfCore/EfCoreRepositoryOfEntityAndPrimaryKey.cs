using System.Linq;
using System.Threading;
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

        public override void Delete(TPrimaryKey id)
        {
            var entity = GetAsync(id).Result;
            dbContext.Remove(entity);
        }

        public override async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsQueryable();
        }

        public override void Insert(TEntity entity)
        {
            dbContext.Add(entity);
        }

        public override void Update(TEntity entity)
        {
            dbContext.Update(entity);
        }
    }

}
