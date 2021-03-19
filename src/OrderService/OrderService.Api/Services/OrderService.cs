using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddOrder(Order Order)
        {
            await Task.Run(() => _unitOfWork.OrderRepository.Insert(Order));
        }

        public async Task AddOrders(List<Order> Orders)
        {
            await Task.Run(() => _unitOfWork.OrderRepository.Insert(Orders));

        }

        public async Task<Order> GetOrder(Expression<Func<Order, bool>> filter, string includeProperties)
        {
            var Orders= await Task.Run(() => _unitOfWork.OrderRepository.Get(filter, includeProperties));

            if (!Orders.Any())
            {
                return null;
            }

            return Orders.FirstOrDefault();

        }

        public async Task<List<Order>> GetOrders(Expression<Func<Order, bool>> filter, string includeProperties)
        {
            return await Task.Run(() => _unitOfWork.OrderRepository.Get(filter, includeProperties));

        }
    }
}
