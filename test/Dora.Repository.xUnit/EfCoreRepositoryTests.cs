using System.Threading.Tasks;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Shouldly;
using System;
using Microsoft.Extensions.DependencyInjection;
using Dora.Repository.EfCore;

namespace Dora.Repository.xUnit
{
    public class EfCoreRepositoryTests : RepositoryTestBase
    {
        readonly IRepository<User> _userRepository;
        readonly IUnitOfWorkManager _unitOfWorkManager;

        public EfCoreRepositoryTests()
        {
            Init();
            _userRepository = GetService<IRepository<User>>();
            _unitOfWorkManager = GetService<IUnitOfWorkManager>();
        }

        private void Init()
        {
            collection.AddTransient(typeof(IDbContextProvider), typeof(DefaultDbContextProvider<MyDb>));
            GetService<EfGenericRepositoryRegistrar>().RegisterForDbContext(typeof(MyDb));
            ReBuild();
        }

        class User : IEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class MyDb: DbContext
        {
            public DbSet<User> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("testdb");
            }
        }

        [Fact]
        public async Task Insert_Entity() 
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                var entity = new User{ Name = "foo" };
                entity.IsTransient().ShouldBeTrue();
                await _userRepository.InsertAsync(entity);
                await uow.SaveChangeAsync();
                entity.IsTransient().ShouldBeFalse();
            }
        }
    }
}