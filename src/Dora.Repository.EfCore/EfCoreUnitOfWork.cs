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

        public Task SaveChangeAsync()
        {
            return dbContext.SaveChangesAsync();
            throw new System.NotImplementedException();
        }
    }
}
