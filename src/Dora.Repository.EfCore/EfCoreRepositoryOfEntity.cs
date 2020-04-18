
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Dora.Repository.Abstract;
using Dora.Repository.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dora.Repository.EfCore
{
    public class EfCoreRepository<TDbContext, TEntity> :
        EfCoreRepository<TDbContext, TEntity, int>, IRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IEntity<int>
    {
        public EfCoreRepository(IDbContextProvider dbContextProvider) : base(dbContextProvider)
        {
            
        }
    }

    public class EfGenericRepositoryRegistrar
    {
        private readonly ServiceCollection _collection;

        public EfGenericRepositoryRegistrar(ServiceCollection collection)
        {
            this._collection = collection;
        }

        public void RegisterForDbContext(Type dbContextType)
        {
            // IRepository<User, int> => Repository<Db, User, int>
            // IRepository<User> => Repository<Db, User>
            foreach (var entityType in GetEntityTypes(dbContextType))
            {
                var primaryKeyType = GetPrimaryKeyType(entityType);
                Type repoType,implType;
                if (primaryKeyType == typeof(int))
                {
                    repoType = typeof(IRepository<>).MakeGenericType(entityType);
                    implType = typeof(EfCoreRepository<,>).MakeGenericType(dbContextType, entityType);
                    _collection.AddTransient(repoType, implType);
                }
                repoType = typeof(IRepository<,>).MakeGenericType(entityType, primaryKeyType);
                implType = typeof(EfCoreRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
                _collection.AddTransient(repoType, implType);
            }
        }

        private IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                && IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                select property.PropertyType.GenericTypeArguments[0];
        }

        public Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }
            return null;
        }

        public bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }
    }
}
