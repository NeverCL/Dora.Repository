using System.Threading.Tasks;
using System;
using System.Threading;

namespace Dora.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangeAsync(CancellationToken cancellationToken);
        Task SaveChangeAsync();
        Task RollBackAsync(CancellationToken cancellationToken);
        Task RollBackAsync();
    }

    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract void Dispose();

        public abstract Task RollBackAsync(CancellationToken cancellationToken);

        public virtual Task RollBackAsync()
            => RollBackAsync(CancellationToken.None);

        public abstract Task SaveChangeAsync(CancellationToken cancellationToken);

        public virtual Task SaveChangeAsync()
            => SaveChangeAsync(CancellationToken.None);
    }
}
