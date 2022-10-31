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

    public abstract class RepoAbstract<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : IDbContext<TEntity>, new()
    {
        internal RepoAbstract()
        {
            DbContext = new TDbContext();
        }

        internal TDbContext DbContext { get; }

        public virtual int Create(TEntity entity)
        {
            return DbContext.Create(entity);
        }

        public virtual int Update(TEntity entity)
        {
            return DbContext.Update(entity);
        }

        public int Delete(TEntity entity)
        {
            return DbContext.Delete(entity);
        }

        public virtual TEntity? FindById(Guid id)
        {
            return DbContext.FindById(id);
        }
    }
}