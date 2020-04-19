using System.Threading.Tasks;
using System;

namespace Dora.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangeAsync();

        Task RollBackAsync();
    }
}
