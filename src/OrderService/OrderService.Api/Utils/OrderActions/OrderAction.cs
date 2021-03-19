using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using OrderService.Api.Model;
using OrderService.Data.Models;

namespace OrderService.Api.Utils.OrderActions
{
    public class OrderAction:IOrderAction
    {
        private readonly IConfiguration _configuration;

        public OrderAction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OrderLine CreateOrderline(CreateProductRequest request, Product product)
        {
            if (product.HasQuantityDiscount && request.Quantity >= 10)
            {
                var quantityDiscountPercentile = Convert.ToInt32(_configuration.GetSection("QuantityDiscountPercentage").Value);
                var discount = (quantityDiscountPercentile * product.Price) / 100;
                product.Price = product.Price - discount;
            }

            if (product.HasFlatDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value))
            {
                var flatDiscount = Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value);
                product.Price = product.Price - flatDiscount;
            }

            if (product.HasDisabilityDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value))
            {
                var disabilityDiscount = Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value);
                product.Price = product.Price - disabilityDiscount;
            }


            var orderline = new OrderLine
            {
                Id = Guid.NewGuid(),
                Product = product,
                Quantity = request.Quantity
            };
            return orderline;
        }

        public Order CreateOrder(CreateOrderRequest request, List<OrderLine> orderlines)
        {
            var order = new Order
            {
                Company = request.CompanyName,
                Id = Guid.NewGuid(),
                OrderLines = orderlines
            };
            return order;
        }
    }
}
