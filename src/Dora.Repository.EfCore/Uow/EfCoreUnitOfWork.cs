using System.Threading.Tasks;
using System.Threading;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class EfCoreUnitOfWork : UnitOfWorkBase
    {
        DbContext dbContext;
        
        public EfCoreUnitOfWork(IDbContextProvider dbContextProvider)
        {
            dbContext = dbContextProvider.GetDbContext();
        }

        public override void Dispose()
        {
            dbContext.Dispose();
        }

        public override Task RollBackAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override async Task SaveChangeAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
