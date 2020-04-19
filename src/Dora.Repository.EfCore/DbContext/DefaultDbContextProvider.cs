using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    {
        static TDbContext _dbContext;
        public DefaultDbContextProvider(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
