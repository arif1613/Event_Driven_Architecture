using System.Collections.Generic;
using OrderService.Api.Model.Request;
using OrderService.Data.Models;

namespace OrderService.Api.Utils.OrderActions
{
    public interface IOrderCalculation
    {
        public OrderLine CreateOrderline(CreateProductRequest request, Product product);
        public Order CreateOrder(CreateOrder request, List<OrderLine> orderlines);

    }
}