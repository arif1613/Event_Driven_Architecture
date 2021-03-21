using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IProductEventService
    {
        Task AddEvent(ProductCreatedEvent Event);
        Task<Product> GetProduct(Expression<Func<ProductCreatedEvent, bool>> filter, string includeProperties);
    }
}
