using System;

namespace IntrepidProducts.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Create(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        TEntity? FindById(Guid id);
    }

    public abstract class AbstractRepo<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected AbstractRepo(ConfigurationManager configManager)
        {
            ConfigurationManager = configManager;
        }

        protected ConfigurationManager ConfigurationManager { get; }

        public abstract int Create(TEntity entity);

        public abstract int Update(TEntity entity);

        public abstract int Delete(TEntity entity);

        public abstract TEntity? FindById(Guid id);
    }
}