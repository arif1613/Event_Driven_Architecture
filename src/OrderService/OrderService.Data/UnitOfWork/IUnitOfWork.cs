using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Receipt> ReceiptRepository { get; }
    }
}