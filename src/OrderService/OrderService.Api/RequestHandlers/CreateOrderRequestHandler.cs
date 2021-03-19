using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Api.Utils.OrderActions;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Order>
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderAction _orderAction;
        public CreateOrderRequestHandler(IProductService productService, IOrderService orderService, IOrderAction orderAction)
        {
            _productService = productService;
            _orderService = orderService;
            _orderAction = orderAction;
        }

        public async Task<Order> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var orderlines = new List<OrderLine>();

            foreach (var product in request.Products)
            {
                var dbproduct =
                    await _productService.GetProduct(
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

        //private OrderLine CreateOrderline(CreateOrderRequest request, Product product)
        //{

        //    if (product.HasQuantityDiscount && request.Quantity >= 10)
        //    {
        //        var quantityDiscountPercentile = Convert.ToInt32(_configuration.GetSection("QuantityDiscountPercentage").Value);
        //        var discount = (quantityDiscountPercentile * product.Price) / 100;
        //        product.Price = product.Price - discount;
        //    }

        //    if (product.HasFlatDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value))
        //    {
        //        var flatDiscount = Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value);
        //        product.Price = product.Price - flatDiscount;
        //    }

        //    if (product.HasDisabilityDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value))
        //    {
        //        var disabilityDiscount = Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value);
        //        product.Price = product.Price - disabilityDiscount;
        //    }


        //    var orderline= new OrderLine
        //    {
        //        Id = Guid.NewGuid(),
        //        Product = product,
        //        Quantity = request.Quantity
        //    };
        //    return orderline;
        //}
    }
}
