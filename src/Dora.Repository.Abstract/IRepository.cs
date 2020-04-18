namespace Dora.Repository.Abstract
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity, TPrimaryKey> : IWriteRepository<TEntity,TPrimaryKey>, IReadRepository<TEntity, TPrimaryKey>, IRepository
            where TEntity : IEntity<TPrimaryKey>
    {
        
    }

    public interface IRepository<TEntity> : IRepository<TEntity,int>
            where TEntity : IEntity<int>
    {
        
    }


}
