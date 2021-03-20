using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Api.Test.TestHelpers;
using OrderService.Data.Models;
using OrderService.Data.Repo;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Test.ServiceTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> orderRepoMock;

        [TestInitialize]
        public void Setup()
        {
            orderRepoMock =RepoTestHelpers.OrderMockRepo();
        }

        [TestMethod]
        public void GetOrders_Will_Return_Correct_Orders()
        {
            var orderService = new Services.OrderService(UnitOfWorkTestHelpers.MockOrderUnitOfWork().Object);
            var orders = orderService.GetOrders(null, null).GetAwaiter().GetResult();
            Assert.IsNotNull(orders);
            Assert.AreEqual(100, orders.Count);
            Assert.AreEqual(orders[0].Company, "testcompany 0");
        }

        [TestMethod]
        public void Add_Order_Will_Add_Correct_Order()
        {

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Company = "newcompany",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        Product = new Product
                        {
                            Id = Guid.NewGuid(),
                            Price = 100,
                            ProductName = "testname",
                            ProductType = "testtype"
                        }
                    }
                }
            };

            
            var orderService = new Services.OrderService(UnitOfWorkTestHelpers.MockOrderUnitOfWork().Object);
            orderService.AddOrder(order).GetAwaiter().GetResult();


            //Assert 
            UnitOfWorkTestHelpers.MockOrderUnitOfWork().Verify();
        }

        [TestCleanup]
        public void Cleanup()
        {
           UnitOfWorkTestHelpers.MockOrderUnitOfWork().Reset();
            orderRepoMock.Reset();
        }




    }
}
