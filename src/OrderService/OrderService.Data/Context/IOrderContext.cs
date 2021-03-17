using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;

namespace OrderService.Data.Context
{
    public interface IOrderContext
    {
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }

        void UpdateEntry<TEntity>(TEntity entity) where TEntity : class;
        void DeleteEntry<TEntity>(TEntity entity) where TEntity : class;
        void SaveDatabase();
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}
