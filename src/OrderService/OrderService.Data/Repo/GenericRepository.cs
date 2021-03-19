using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Data.Context;

namespace OrderService.Data.Repo
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IOrderContext context;
        public GenericRepository(IOrderContext context)
        {
            this.context = context;
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> filter=null, string includeProperties = null)
        {
            IQueryable<TEntity> query = context.GetDbSet<TEntity>();
            if (filter!=null)
            {
                query = query.Where(filter);
            }
            if (includeProperties == null) return query.ToList();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                context.GetDbSet<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            context.SaveDatabase();
        }

        public void Insert(List<TEntity> entities)
        {

            try
            {
                context.GetDbSet<TEntity>().AddRange(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
            context.SaveDatabase();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = context.GetDbSet<TEntity>().Find(id);
            context.DeleteEntry(entityToDelete);
            context.SaveDatabase();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            context.GetDbSet<TEntity>().Attach(entityToUpdate);
            context.UpdateEntry(entityToUpdate);
            context.SaveDatabase();
        }
    }
}