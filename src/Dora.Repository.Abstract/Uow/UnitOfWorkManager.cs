using System;

namespace Dora.Repository.Abstract
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private IUnitOfWork _uow;
        private readonly IServiceProvider serviceProvider;

        public UnitOfWorkManager(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IUnitOfWork Current => _uow;

        public IUnitOfWork Begin()
        {
            return _uow = serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
        }
    }
}
