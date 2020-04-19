using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public interface IDbContextProvider
    {
        DbContext GetDbContext();
    }
}
