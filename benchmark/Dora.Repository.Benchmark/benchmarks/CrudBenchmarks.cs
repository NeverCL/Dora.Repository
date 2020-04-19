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

        public CrudBenchmarks()
        {
            sp = default(ServiceCollection).AddEfCoreRepository<MyDb>().BuildServiceProvider();
        }

        [Benchmark]
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
            var rep = sp.GetService<IRepository<User>>();
            using (var uow = sp.GetService<IUnitOfWork>())
            {
                rep.Insert(new User { Name = "1" });
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
