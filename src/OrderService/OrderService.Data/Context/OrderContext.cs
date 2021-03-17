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
        public DbSet<Pin> Pins { get; set; }
        public int SaveDatabase()
        {
            return SaveChanges();
        }

        public void UpdateEntry(Pin pin)
        {
            Entry(pin).State = EntityState.Modified;
            SaveChanges();
        }

        public OrderContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
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
