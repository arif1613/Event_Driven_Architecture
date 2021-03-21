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
        private GenericRepository<OrderCreatedEvent> _ordercreated;
        private GenericRepository<ProductCreatedEvent> _productcreated;



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

        public IRepository<OrderCreatedEvent> OrderCreatedEventRepository
        {
            get
            {
                return _ordercreated ??= new GenericRepository<OrderCreatedEvent>(_dbContext);
            }
        }

        public IRepository<ProductCreatedEvent> ProductCreatedEventRepository
        {
            get
            {
                return _productcreated ??= new GenericRepository<ProductCreatedEvent>(_dbContext);
            }
        }

    }
}
