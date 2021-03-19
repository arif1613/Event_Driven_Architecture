using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderService.Data.Models;

namespace OrderService.Api.Services
{
    public interface IOrderService
    {
        Task AddOrder(Order Order);
        Task AddOrders(List<Order> Orders);

        Task<Order> GetOrder(Expression<Func<Order, bool>> filter, string includeProperties);
        Task<List<Order>> GetOrders(Expression<Func<Order, bool>> filter, string includeProperties);

    }
}
