using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IOrderEventService
    {
        Task AddEvent(OrderCreatedEvent Event);
        Task<List<OrderCreatedEvent>> GetEventStoreOrders(Expression<Func<OrderCreatedEvent, bool>> filter, string includeProperties);
        Task<OrderCreatedEvent> GetEventStoreOrder(Expression<Func<OrderCreatedEvent, bool>> filter, string includeProperties);

    }
}
