using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Api.Model;
using OrderService.Api.Utils.OrderActions;
using OrderService.Data.Models;


namespace OrderService.Api.Test.UtilsTests
{
    [TestClass]
    public class OrderCalculationTests
    {
        private CreateProductRequest request;
        private IConfiguration _Configuration;
        private Product product1;
        private Product product2;
        private Product product3;
        private Product product4;



        [TestInitialize]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"QuantityDiscountPercentage", "50"},
                {"FlatDiscount", "100"},
                {"DisabilityDiscount", "1000"}
            };

            _Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            request = new CreateProductRequest
            {
                ProductName = "test product name",
                ProductType = "test product type",
                Quantity = 5
            };

            product1 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "test product name",
                ProductType = "test product type",
                Price = 500,
                HasDisabilityDiscount = false,
                HasFlatDiscount = true,
                HasQuantityDiscount = false
            };

            product2 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "test product name",
                ProductType = "test product type",
                Price = 300,
                HasDisabilityDiscount = false,
                HasFlatDiscount = false,
                HasQuantityDiscount = true
            };

            product3 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "test product name",
                ProductType = "test product type",
                Price = 1500,
                HasDisabilityDiscount = true,
                HasFlatDiscount = false,
                HasQuantityDiscount = false
            };

            product4 = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "test product name",
                ProductType = "test product type",
                Price = 2500,
                HasDisabilityDiscount = true,
                HasFlatDiscount = true,
                HasQuantityDiscount = true
            };
        }

        [TestMethod]
        public void OrderLine_Will_have_flat_Discount()
        {
            var orderaction=new OrderCalculation(_Configuration);
            var orderline = orderaction.CreateOrderline(request, product1);

            Assert.IsNotNull(orderline);
            Assert.AreEqual(2400,orderline.TotalPrice);

        }

        [TestMethod]
        public void OrderLine_Will_not_have_Quantity_Discount_for_less_order()
        {
            var orderaction = new OrderCalculation(_Configuration);
            var orderline = orderaction.CreateOrderline(request, product2);

            Assert.IsNotNull(orderline);
            Assert.AreEqual(300, orderline.Product.Price);
            Assert.AreEqual(1500, orderline.TotalPrice);
        }

        [TestMethod]
        public void OrderLine_Will_have_Quantity_Discount_for_higher_order()
        {
            request.Quantity = 10;
            var orderaction = new OrderCalculation(_Configuration);
            var orderline = orderaction.CreateOrderline(request, product2);

            Assert.IsNotNull(orderline);
            Assert.AreEqual(150, orderline.Product.Price);
            Assert.AreEqual(1500, orderline.TotalPrice);
        }

        [TestMethod]
        public void OrderLine_Will_have_Disability_Discount()
        {
            var orderaction = new OrderCalculation(_Configuration);
            var orderline = orderaction.CreateOrderline(request, product3);

            Assert.IsNotNull(orderline);
            Assert.AreEqual(6500, orderline.TotalPrice);

        }

        [TestMethod]
        public void OrderLine_Will_have_All_Discount()
        {
            request.Quantity = 14;
            var orderaction = new OrderCalculation(_Configuration);
            var orderline = orderaction.CreateOrderline(request, product4);

            Assert.IsNotNull(orderline);
            Assert.AreEqual(16400, orderline.TotalPrice);

        }
    }
}
