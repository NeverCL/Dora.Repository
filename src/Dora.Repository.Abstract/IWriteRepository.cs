using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dora.Repository.Abstract
{
    public interface IWriteRepository<TEntity,TPrimaryKey> : IRepository
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}
