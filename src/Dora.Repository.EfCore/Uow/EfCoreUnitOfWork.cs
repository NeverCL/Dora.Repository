using System.Threading.Tasks;
using System.Threading;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public class EfCoreUnitOfWork : UnitOfWorkBase
    {
        private readonly IDbContextProvider dbContextProvider;
        DbContext dbContext => dbContextProvider.GetDbContext();
        
        public EfCoreUnitOfWork(IDbContextProvider dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        public override void Dispose()
        {
            dbContextProvider.Dispose();
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
