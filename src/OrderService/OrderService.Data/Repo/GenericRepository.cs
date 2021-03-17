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
        public IEnumerable<TEntity> Get(string includeProperties=null)
        {
            IQueryable<TEntity> query = context.GetDbSet<TEntity>();

            if (includeProperties!=null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            return query.ToList();
        }

        public TEntity Get(object id)
        {
            return context.GetDbSet<TEntity>().Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            context.GetDbSet<TEntity>().Add(entity);
            context.SaveDatabase();
        }

        public void Insert(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                try
                {
                    context.GetDbSet<TEntity>().Add(entity);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
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