using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderService.Data.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties);
        void Insert(TEntity entity);
        void Insert(List<TEntity> entities);

        void Update(TEntity entityToUpdate);
        void Delete(object id);
    }
}