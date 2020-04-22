using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dora.Repository.Benchmark.Benchmarks
{
    [MinColumn, MaxColumn]
    public class CrudBenchmarks
    {
        private readonly ServiceProvider sp;

        public IRepository<User> userRep { get; }

        private readonly IUnitOfWorkManager uowManager;

        public CrudBenchmarks()
        {
            sp = default(ServiceCollection).AddEfCoreRepository<MyDb>().BuildServiceProvider();
            userRep = sp.GetService<IRepository<User>>();
            uowManager = sp.GetService<IUnitOfWorkManager>();
        }

        [Benchmark(Baseline = true)]
        public async Task NativeInsert()
        {
            using (var db = new MyDb())
            {
                db.Add(new User { Name = "1" });
                await db.SaveChangesAsync();
            }
        }

        [Benchmark]
        public async Task RepositoryInsertAsync()
        {
            using (var uow = uowManager.Begin())
            {
                userRep.Insert(new User { Name = "1" });
                await uow.SaveChangeAsync();
            }
        }
    }
        public class User : IEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class MyDb: DbContext
        {
            public DbSet<User> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("testdb");
            }
        }
}
