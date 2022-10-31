﻿using System.Linq;
using IntrepidProducts.Common;
using Microsoft.EntityFrameworkCore;

namespace IntrepidProducts.Repo
{
    public static class DbContextExtensions
    {
        public static bool DetachLocal<TEntity>
            (this DbContext dbContext, TEntity entity, EntityState state)
                where TEntity : class, IHasId
        {
            var local = dbContext.Set<TEntity>().Local
                .FirstOrDefault(x => x.Id == entity.Id);

            if (local == null)
            {
                return false;
            }

            dbContext.Entry(local).State = EntityState.Detached;
            dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}