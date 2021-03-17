using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;

namespace OrderService.Data.Context
{
    public interface IOrderContext
    {
        public DbSet<Pin> Pins { get; set; }
        public int SaveDatabase();
        public void UpdateEntry(Pin pin);

    }
}
