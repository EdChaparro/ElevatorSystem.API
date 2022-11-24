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
        protected abstract bool DoesEntityExits(TEntity entity);

        public int Create(TEntity entity)
        {
            if (DoesEntityExits(entity))
            {
                return 0;
            }

            var result = DoCreate(entity);

            if (result > 1)
            {
                OnSuccessfulCreate(entity);
            }

            return result;
        }

        public abstract int DoCreate(TEntity entity);

        public virtual void OnSuccessfulCreate(TEntity entity)
        { }

        public abstract int Update(TEntity entity);

        public abstract int Delete(TEntity entity);

        public abstract TEntity? FindById(Guid id);

        public abstract IEnumerable<TEntity> FindAll();
    }
}