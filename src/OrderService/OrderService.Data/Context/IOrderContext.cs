using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;

namespace OrderService.Data.Context
{
    public interface IOrderContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<OrderCreatedEvent> MessageCreatedEvents { get; set; }
        public DbSet<ProductCreatedEvent> ProductCreatedEvents { get; set; }
        EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class;
        void UpdateEntry<TEntity>(TEntity entity) where TEntity : class;
        void DeleteEntry<TEntity>(TEntity entity) where TEntity : class;
        void SaveDatabase();
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    }
}
