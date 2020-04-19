using System.Threading.Tasks;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        DbContext dbContext;
        
        public EfCoreUnitOfWork(IDbContextProvider dbContextProvider)
        {
            dbContext = dbContextProvider.GetDbContext();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public Task RollBackAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task SaveChangeAsync()
        {
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
