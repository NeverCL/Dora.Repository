using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class EfCoreRepository<TDbContext, TEntity> :
        EfCoreRepository<TDbContext, TEntity, int>, IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity<int>
    {
        public EfCoreRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
            
        }
    }
}