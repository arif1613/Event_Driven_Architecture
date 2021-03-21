using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Request;
using OrderService.Api.Services;
using OrderService.Api.Utils.OrderActions;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrder, Order>
    {

        private readonly IProductEventService _productEventService;

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderCalculation _orderAction;
        public CreateOrderRequestHandler(IProductService productService, IOrderService orderService, IOrderCalculation orderAction)
        {
            _productService = productService;
            _orderService = orderService;
            _orderAction = orderAction;
        }

        public async Task<Order> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var orderlines = new List<OrderLine>();

            foreach (var product in request.Products)
            {
                var dbproduct = await _productService.GetProduct(
                        r => r.ProductName == product.ProductName && r.ProductType == product.ProductType, null);
                if (dbproduct == null)
                {
                    return null;
                }
                orderlines.Add(_orderAction.CreateOrderline(product, dbproduct));
            }

            var order = _orderAction.CreateOrder(request, orderlines);
            await _orderService.AddOrder(order);
            return order;
        }
    }
}
