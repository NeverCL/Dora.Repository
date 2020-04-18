using System.Threading.Tasks;
using System;

namespace Dora.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangeAsync();

        Task RollBackAsync();
    }

    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get; }
        IUnitOfWork Begin();
    }

    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkManager(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public IUnitOfWork Current => _uow;

        public IUnitOfWork Begin()
        {
            return _uow;
        }
    }
}
