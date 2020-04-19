using System.Threading.Tasks;
using Dora.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Shouldly;

namespace Dora.Repository.xUnit
{
    public class EfCoreRepositoryTests : RepositoryTestBase<EfCoreRepositoryTests.MyDb>
    {
        readonly IRepository<User> _userRepository;
        readonly IUnitOfWorkManager _unitOfWorkManager;
        public EfCoreRepositoryTests()
        {
            _userRepository = GetService<IRepository<User>>();
            _unitOfWorkManager = GetService<IUnitOfWorkManager>();
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

        [Fact]
        public async Task Insert_Entity() 
        {
            await WatchInvokeAsync(async () => {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    // insert
                    var user = new User{ Name = "foo" };
                    user.IsTransient().ShouldBeTrue();
                    _userRepository.Insert(user);
                    await uow.SaveChangeAsync();
                    user.IsTransient().ShouldBeFalse();
                    
                    // update
                    user.Name = System.Guid.NewGuid().ToString();
                    _userRepository.Update(user);
                    await uow.SaveChangeAsync();
                    var entity = await _userRepository.GetAsync(user.Id);   // get
                    entity.Name.ShouldBe(user.Name);

                    // delete
                    _userRepository.Delete(user.Id);
                    await uow.SaveChangeAsync();
                    var count = await _userRepository.GetAll().CountAsync(); // getall
                    count.ShouldBe(0);
                }
            });
        }
    }
}