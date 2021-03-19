using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Data.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private IOrderContext _dbContext;
        private GenericRepository<Order> _orders;
        private GenericRepository<Product> _products;
        private GenericRepository<Receipt> _receipts;


        public UnitOfWork(IOrderContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRepository<Order> OrderRepository
        {
            get
            {
                return _orders ??= new GenericRepository<Order>(_dbContext);
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                return _products ??= new GenericRepository<Product>(_dbContext);
            }
        }

        public IRepository<Receipt> ReceiptRepository
        {
            get
            {
                return _receipts ??= new GenericRepository<Receipt>(_dbContext);
            }
        }

    }
}
