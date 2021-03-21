using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Services
{
    public class ProductEventService:IProductEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddEvent(ProductCreatedEvent Event)
        {
            await Task.Run(() => _unitOfWork.ProductCreatedEventRepository.Insert(Event));
        }

        public async Task<Product> GetProduct(Expression<Func<ProductCreatedEvent, bool>> filter, string includeProperties)
        {
            var events = await Task.Run(() => _unitOfWork.ProductCreatedEventRepository.Get(filter, includeProperties));

            if (!events.Any())
            {
                return null;
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                HasDisabilityDiscount = events.FirstOrDefault().HasDisabilityDiscount,
                HasFlatDiscount = events.FirstOrDefault().HasFlatDiscount,
                HasQuantityDiscount = events.FirstOrDefault().HasQuantityDiscount,
                Price = events.FirstOrDefault().Price,
                ProductName = events.FirstOrDefault().ProductName,
                ProductType = events.FirstOrDefault().ProductType
            };

            return product;
        }
    }
}
