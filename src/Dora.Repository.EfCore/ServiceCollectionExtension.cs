using Dora.Repository.Abstract;
using Dora.Repository.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static ServiceCollection AddEfCoreRepository<TDbContext>(this ServiceCollection collection)
            where TDbContext : DbContext
        {
            collection = collection ?? new ServiceCollection();
            collection.AddTransient<DbContext, TDbContext>();
            collection.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();
            collection.AddTransient<IUnitOfWorkManager, UnitOfWorkManager>();
            collection.AddTransient(typeof(IDbContextProvider), typeof(DefaultDbContextProvider<DbContext>));
            foreach (var item in new EfGenericRepositoryRegistrar().RegisterForDbContext<TDbContext>())
                collection.AddTransient(item.svcType, item.impType);
            return collection;
        }
    }
}