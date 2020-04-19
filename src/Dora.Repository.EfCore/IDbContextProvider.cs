using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public interface IDbContextProvider
    {
        DbContext GetDbContext();
    }

    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    {
        static ThreadLocal<TDbContext> _dbContext = new ThreadLocal<TDbContext>();
        public DefaultDbContextProvider(TDbContext dbContext)
        {
            if(_dbContext.IsValueCreated)
                return;                
            _dbContext.Value = dbContext;
        }

        public DbContext GetDbContext()
        {
            return _dbContext.Value;
        }
    }
}
