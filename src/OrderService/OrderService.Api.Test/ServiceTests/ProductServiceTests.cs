using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Api.Services;
using OrderService.Api.Test.TestHelpers;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Api.Test.ServiceTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IRepository<Product>> ProductRepoMock;

        [TestInitialize]
        public void Setup()
        {
            ProductRepoMock =RepoTestHelpers.ProductMockRepo();
        }

        [TestMethod]
        public void GetProducts_Will_Return_Correct_Products()
        {
            var ProductService = new ProductService(UnitOfWorkTestHelpers.MockProductUnitOfWork().Object);
            var Products = ProductService.GetProducts(null, null).GetAwaiter().GetResult();
            Assert.IsNotNull(Products);
            Assert.AreEqual(100, Products.Count);
        }

        [TestMethod]
        public void Add_Product_Will_Add_Correct_Product()
        {

            var Product = new Product
            {
                Id = Guid.NewGuid(),
                Price = 100,
                ProductName = "testname",
                ProductType = "testtype"
            };

            
            var ProductService = new ProductService(UnitOfWorkTestHelpers.MockProductUnitOfWork().Object);
            ProductService.AddProduct(Product).GetAwaiter().GetResult();


            //Assert 
            UnitOfWorkTestHelpers.MockProductUnitOfWork().Verify();
        }

        [TestCleanup]
        public void Cleanup()
        {
           UnitOfWorkTestHelpers.MockProductUnitOfWork().Reset();
            ProductRepoMock.Reset();
        }




    }
}
