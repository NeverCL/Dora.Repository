using System;
using System.Threading.Tasks;
using Dora.Repository.Abstract;
using Dora.Repository.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dora.Repository.xUnit
{
    public abstract class RepositoryTestBase<TDbContext>
        where TDbContext : DbContext
    {
        private readonly ServiceProvider sp;
        
        public RepositoryTestBase()
        {
            sp = default(ServiceCollection).AddEfCoreRepository<TDbContext>().BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return sp.GetService<T>();
        }

        protected async Task WatchInvokeAsync(Func<Task> func, int times = 3)
        {
            for (int i = 0; i < times; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Console.WriteLine(i + " begin :" + watch.ElapsedMilliseconds);
                await func();
                Console.WriteLine(i + " end :" + watch.ElapsedMilliseconds);
            }
        }
    }
}