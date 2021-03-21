using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using OrderService.Data.Models;

namespace OrderService.Data.Context
{
    public sealed class OrderContext: DbContext,IOrderContext
    {

        public OrderContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<OrderCreatedEvent> MessageCreatedEvents { get; set; }
        public DbSet<ProductCreatedEvent> ProductCreatedEvents { get; set; }

        public void SaveDatabase()
        {
            SaveChanges();
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }

        public void UpdateEntry<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntry<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dbPath = Path.Combine(FileSystem.CurrentDirectory, "OrdersDb.db3");
            try
            {
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

      
    }
}
