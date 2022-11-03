using System;
using System.Collections.Generic;

namespace IntrepidProducts.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Create(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        TEntity? FindById(Guid id);
        IEnumerable<TEntity> FindAll(); //TODO: Should this be part of a different interface?
    }

    public abstract class AbstractRepo<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public abstract int Create(TEntity entity);

        public abstract int Update(TEntity entity);

        public abstract int Delete(TEntity entity);

        public abstract TEntity? FindById(Guid id);

        public abstract IEnumerable<TEntity> FindAll();
    }
}