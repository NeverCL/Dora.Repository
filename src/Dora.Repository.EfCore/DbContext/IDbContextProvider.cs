using System;
using Microsoft.EntityFrameworkCore;

namespace Dora.Repository.EfCore
{
    public interface IDbContextProvider : IDisposable
    {
        DbContext GetDbContext();
    }
}
