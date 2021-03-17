using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Api.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private IOrderContext _dbContext;
        private GenericRepository<OrderLine> _orderlines;
        private GenericRepository<Product> _products;

        public UnitOfWork(IOrderContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRepository<OrderLine> OrderLineRepository
        {
            get
            {
                return _orderlines ??= new GenericRepository<OrderLine>(_dbContext);
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                return _products ??= new GenericRepository<Product>(_dbContext);
            }
        }
    }
}
