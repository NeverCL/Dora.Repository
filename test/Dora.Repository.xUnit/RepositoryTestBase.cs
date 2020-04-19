using System;
using Dora.Repository.Abstract;
using Dora.Repository.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dora.Repository.xUnit
{
    public abstract class RepositoryTestBase<TDbContext>
        where TDbContext : DbContext
    {
        private ServiceProvider sp;
        protected readonly ServiceCollection collection;

        public RepositoryTestBase()
        {
            collection = new ServiceCollection();
            collection.AddTransient<IUnitOfWork,EfCoreUnitOfWork>();
            collection.AddTransient<IUnitOfWorkManager,UnitOfWorkManager>();
            collection.AddTransient<EfGenericRepositoryRegistrar>();
            collection.AddTransient<ServiceCollection>(s => collection);
            collection.AddTransient<DbContext, TDbContext>();
            collection.AddTransient(typeof(IDbContextProvider), typeof(DefaultDbContextProvider<DbContext>));
            ReBuild();
            GetService<EfGenericRepositoryRegistrar>().RegisterForDbContext(typeof(TDbContext));
            ReBuild();
        }

        protected void ReBuild()
        {
            sp = collection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return sp.GetService<T>();
        }
    }
}