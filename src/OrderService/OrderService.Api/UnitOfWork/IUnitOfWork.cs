using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Api.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<OrderLine> OrderLineRepository { get; }
        IRepository<Product> ProductRepository { get; }
    }
}