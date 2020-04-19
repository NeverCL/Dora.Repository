namespace Dora.Repository.Abstract
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Current { get; }
        IUnitOfWork Begin();
    }
}
