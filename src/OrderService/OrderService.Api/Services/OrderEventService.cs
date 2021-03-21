using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Services
{
    public class OrderEventService: IOrderEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddEvent(OrderCreatedEvent Event)
        {
            await Task.Run(() => _unitOfWork.OrderCreatedEventRepository.Insert(Event));

        }

        public async Task<List<OrderCreatedEvent>> GetEventStoreOrders(Expression<Func<OrderCreatedEvent, bool>> filter, string includeProperties)
        {
            return await Task.Run(() => _unitOfWork.OrderCreatedEventRepository.Get(filter, includeProperties));
        }

        public async Task<OrderCreatedEvent> GetEventStoreOrder(Expression<Func<OrderCreatedEvent, bool>> filter, string includeProperties)
        {
            var orders= await Task.Run(() => _unitOfWork.OrderCreatedEventRepository.Get(filter, includeProperties));

            if (!orders.Any())
            {
                return null;
            }

            return orders.FirstOrDefault();
        }
    }
}
