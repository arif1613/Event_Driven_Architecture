using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Data.Test.RepoTests
{
    [TestClass]
    public class OrderRepoTests
    {

        private Mock<IOrderContext> orderContextMock;

        [TestInitialize]
        public void Setup()
        {
            orderContextMock = TestHelpers.TestHelpers.MockContext<IOrderContext>();
            var Orders = Enumerable.Range(0, 100).Select(r => new Order
            {
                Id = Guid.NewGuid(),
                Company = $"testcompany {r}",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        Product = new Product
                        {
                            HasDisabilityDiscount = true,
                            HasFlatDiscount = true,
                            HasQuantityDiscount = true,
                            Id = Guid.NewGuid(),
                            Price = 800+r,
                            ProductName = $"test_product_name {r}",
                            ProductType = $"test_product_type {r}"
                        },
                        Quantity = r
                    },
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        Product = new Product
                        {
                            HasDisabilityDiscount = false,
                            HasFlatDiscount = false,
                            HasQuantityDiscount = true,
                            Id = Guid.NewGuid(),
                            Price = 50+r,
                            ProductName = $"test_product_name2 {r}",
                            ProductType = $"test_product_type2 {r}"
                        },
                        Quantity = r
                    }
                }
            });

            var orderMockSet = TestHelpers.TestHelpers.CreateMockDbSet(Orders.AsQueryable());
            orderContextMock.Setup(r => r.GetDbSet<Order>()).Returns(orderMockSet.Object);

        }

        [TestMethod]
        public void Get_Will_Return_Correct_Orders()
        {
            var ordersRepo = new GenericRepository<Order>(orderContextMock.Object);
            var orders = ordersRepo.Get(null, null);
            Assert.IsNotNull(orders);    
            Assert.AreEqual(100, orders.Count);
            Assert.AreEqual(orders[0].Company, "testcompany 0");
            Assert.AreEqual(orders[11].Company, "testcompany 11");
        }


        [TestMethod]
        public void Get_Will_Return_Correct_Filtered_Orders()
        {
            var ordersRepo = new GenericRepository<Order>(orderContextMock.Object);
            var orders = ordersRepo.Get(r=>r.Company== "testcompany 20", null);
            Assert.IsNotNull(orders);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(orders[0].Company, "testcompany 20");
        }

        [TestCleanup]
        public void Cleanup()
        {
            orderContextMock.Reset();
        }
    }
}
