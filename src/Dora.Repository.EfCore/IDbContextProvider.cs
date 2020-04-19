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
        // static AsyncLocal<TDbContext> _dbContext = new AsyncLocal<TDbContext>();
        static TDbContext _dbContext;
        public DefaultDbContextProvider(TDbContext dbContext)
        {
            // if(_dbContext.Value != null)
            //     return;
            // _dbContext.Value = dbContext;
            _dbContext = dbContext;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
