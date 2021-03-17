using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderService.Data.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(string includeProperties=null);
        TEntity Get(object id);
        void Insert(TEntity entity);
        void Insert(List<TEntity> entities);

        void Update(TEntity entityToUpdate);
        void Delete(object id);
    }
}