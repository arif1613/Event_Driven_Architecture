using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Api.Test.TestHelpers
{
    public class RepoTestHelpers
    {
        public static Mock<IOrderContext> OrderContextMock;
        public static Mock<DbSet<Order>> OrderMockSet;
        public static Mock<IRepository<Order>> OrderRepo;


        public static Mock<IRepository<Order>> OrderMockRepo()
        {
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

            OrderContextMock = ContextTestHelpers.MockContext<IOrderContext>();
            OrderMockSet = ContextTestHelpers.CreateMockDbSet(Orders.AsQueryable());
            OrderContextMock.Setup(r => r.GetDbSet<Order>()).Returns(OrderMockSet.Object);
            OrderRepo = new Mock<IRepository<Order>>();
            OrderRepo.Setup(r => r.Get(null, null)).Returns(Orders.ToList());

            return OrderRepo;
        }

        public static Mock<IRepository<Product>> ProductMockRepo()
        {
            var Products = Enumerable.Range(0, 100).Select(r => new Product
            {
                Id = Guid.NewGuid(),
                ProductName = $"product name {r}",
                Price = 500 + r,
                HasDisabilityDiscount = true,
                HasFlatDiscount = true,
                HasQuantityDiscount = true

            });

            var productContextMock = ContextTestHelpers.MockContext<IOrderContext>();
            var productMockSet = ContextTestHelpers.CreateMockDbSet(Products.AsQueryable());
            productContextMock.Setup(r => r.GetDbSet<Product>()).Returns(productMockSet.Object);
            var productRepo = new Mock<IRepository<Product>>();
            productRepo.Setup(r => r.Get(null, null)).Returns(Products.ToList());

            return productRepo;
        }
    }
}
