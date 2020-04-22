using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider
        where TDbContext : DbContext
    {
        readonly static AsyncLocal<TDbContext> _dbContext = new AsyncLocal<TDbContext>();
        private readonly IServiceProvider sp;

        public DefaultDbContextProvider(IServiceProvider sp)
        {
            this.sp = sp;
        }
        
        public void Dispose()
        {
            _dbContext.Value?.DisposeAsync();
            _dbContext.Value = null;
        }

        public DbContext GetDbContext()
        {
             if (_dbContext.Value != null)
                return _dbContext.Value;
            _dbContext.Value = sp.GetService(typeof(TDbContext)) as TDbContext;
            return _dbContext.Value;
        }
    }
}
