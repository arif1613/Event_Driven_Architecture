using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderService.Api.Controllers;
using OrderService.Api.Model.Request;
using OrderService.Api.Services;
using OrderService.Api.Test.TestHelpers;
using OrderService.Data.Models;

namespace OrderService.Api.Test.ControllerTests
{
    [TestClass]
    public class AdminControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private Mock<IReceiptService> _receiptServiceMock;
        private Mock<IOrderService> _orderServiceMock;
        private Mock<IMediator> _mediatorMock;

        [TestInitialize]
        public void Setup()
        {
            _productServiceMock = ServiceTestHelpers.MockService<IProductService>();
            _receiptServiceMock = ServiceTestHelpers.MockService<IReceiptService>();
            _orderServiceMock = ServiceTestHelpers.MockService<IOrderService>();
            _mediatorMock = ServiceTestHelpers.MockService<IMediator>();
        }

        [TestMethod]
        public void Will_return_badrequest_for_invalid_request()
        {
            var admincontroller = new AdminController(_productServiceMock.Object, _receiptServiceMock.Object,
                _orderServiceMock.Object, _mediatorMock.Object);
            admincontroller.ModelState.AddModelError("","invalid input");
            var actionResult = admincontroller.AddProduct(It.IsAny<CreateProduct>()).Result as BadRequestObjectResult;
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void Mediator_will_send_request_message()
        {
            var createproduct = new CreateProduct
            {
                HasDisabilityDiscount = true,
                HasFlatDiscount = true,
                HasQuantityDiscount = true,
                ProductName = "productname",
                ProductType = "producttype",
                Price = 1000
            };

            _mediatorMock.Setup(r=>r.Send(It.IsAny<CreateProduct>(), default)).Verifiable();
            var admincontroller = new AdminController(_productServiceMock.Object, _receiptServiceMock.Object,
                _orderServiceMock.Object, _mediatorMock.Object);
            var actionResult = admincontroller.AddProduct(createproduct).Result as OkObjectResult;
            _mediatorMock.Verify();

        }
    }
}
