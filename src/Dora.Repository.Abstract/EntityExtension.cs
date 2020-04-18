using System;
using System.Collections.Generic;

namespace Dora.Repository.Abstract
{
    public static class EntityExtension
    {
        public static bool IsTransient<TPrimaryKey>(this IEntity<TPrimaryKey> entity)
        {
            var id = entity.Id;
            if (EqualityComparer<TPrimaryKey>.Default.Equals(id, default(TPrimaryKey)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(id) <= 0;
            }

            return false;
        }
    }
}
