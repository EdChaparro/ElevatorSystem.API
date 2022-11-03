using System.Collections.Generic;

namespace IntrepidProducts.Repo
{
    public interface IFindAll<out TEntity> where TEntity : class
    {
        IEnumerable<TEntity> FindAll();
    }
}