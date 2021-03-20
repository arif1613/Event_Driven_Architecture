using System;
using System.Collections.Generic;
using System.Data;
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
    public class ProductRepoTests
    {

        private Mock<IOrderContext> orderContextMock;

        [TestInitialize]
        public void Setup()
        {
            orderContextMock = TestHelpers.TestHelpers.MockContext<IOrderContext>();
            var Products = Enumerable.Range(0, 100).Select(r => new Product
            {
              Id = Guid.NewGuid(),
              ProductName = $"product name {r}",
              Price = 500+r,
              HasDisabilityDiscount = true,
              HasFlatDiscount = true,
              HasQuantityDiscount = true
              
            });

            var productMockSet = TestHelpers.TestHelpers.CreateMockDbSet(Products.AsQueryable());
            orderContextMock.Setup(r => r.GetDbSet<Product>()).Returns(productMockSet.Object);

        }

        [TestMethod]
        public void Get_Will_Return_Correct_Products()
        {
            var productRepo = new GenericRepository<Product>(orderContextMock.Object);
            var products = productRepo.Get(null, null);
            Assert.IsNotNull(products);    
            Assert.AreEqual(100, products.Count);
            Assert.AreEqual(products[11].ProductName, "product name 11");
            Assert.AreEqual(products[20].Price, 500+20);

        }


        [TestMethod]
        public void Get_Will_Return_Correct_Filtered_Products()
        {
            var productRepo = new GenericRepository<Product>(orderContextMock.Object);
            var products = productRepo.Get(r=>r.ProductName== "product name 20", null);
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(products[0].ProductName, "product name 20");
        }


        [TestMethod]
        public void Add_Will_Return_Correct__Product()
        {
            var productRepo = new GenericRepository<Product>(orderContextMock.Object);
            var products = productRepo.Get(r => r.ProductName == "product name 20", null);
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(products[0].ProductName, "product name 20");
        }


        [TestCleanup]
        public void Cleanup()
        {
            orderContextMock.Reset();
        }
    }
}
