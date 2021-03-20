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
    public class OrderCalculation:IOrderCalculation
    {
        private readonly IConfiguration _configuration;

        public OrderCalculation(IConfiguration configuration)
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

            int totalPrice = 0;
            for (int i = 0; i < request.Quantity; i++)
            {
                totalPrice = totalPrice + product.Price;
            }


            if (product.HasFlatDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value))
            {
                var flatDiscount = Convert.ToInt32(_configuration.GetSection("FlatDiscount").Value);
                totalPrice = totalPrice - flatDiscount;
            }

            if (product.HasDisabilityDiscount && product.Price >= Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value))
            {
                var disabilityDiscount = Convert.ToInt32(_configuration.GetSection("DisabilityDiscount").Value);
                totalPrice = totalPrice - disabilityDiscount;
            }

           

            var orderline = new OrderLine
            {
                Id = Guid.NewGuid(),
                Product = product,
                Quantity = request.Quantity,
                TotalPrice = totalPrice
            };
            return orderline;
        }

        public Order CreateOrder(CreateOrderRequest request, List<OrderLine> orderlines)
        {
            int totalPrice = 0;
            foreach (var orderline in orderlines)
            {
                    totalPrice = totalPrice + orderline.TotalPrice;
            }

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
