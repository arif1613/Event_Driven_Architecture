using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using OrderService.Data.Models;
using OrderService.Data.Repo;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api.Test.TestHelpers
{
    public class UnitOfWorkTestHelpers
    {
        public static Mock<IRepository<Order>> orderMockRepo;
        public static Mock<IRepository<Product>> productMockRepo;

        public static Mock<IUnitOfWork> MockOrderUnitOfWork()
        {
            orderMockRepo = RepoTestHelpers.OrderMockRepo();
            var mockUnitofwork=new Mock<IUnitOfWork>();
            mockUnitofwork.Setup(r => r.OrderRepository).Returns(orderMockRepo.Object);
            mockUnitofwork.Setup(r => r.OrderRepository.Insert(It.IsAny<Order>())).Verifiable();
            return mockUnitofwork;
        }

        public static Mock<IUnitOfWork> MockProductUnitOfWork()
        {
            productMockRepo = RepoTestHelpers.ProductMockRepo();

            var mockUnitofwork = new Mock<IUnitOfWork>();
            mockUnitofwork.Setup(r => r.ProductRepository).Returns(productMockRepo.Object);
            mockUnitofwork.Setup(r => r.ProductRepository.Insert(It.IsAny<Product>())).Verifiable();

            return mockUnitofwork;
        }
    }
}
