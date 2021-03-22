using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Event;
using OrderService.Api.Model.Request;
using OrderService.Api.Model.ViewModel;
using OrderService.Api.Services;
using OrderService.Data.Models;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IReceiptService _receiptService;
        private readonly IOrderService _orderService;
        private readonly IMediator _mediator;
        public AdminController(IProductService productService, IReceiptService receiptService, IOrderService orderService, IMediator mediator)
        {
            _productService = productService;
            _receiptService = receiptService;
            _orderService = orderService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getproducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProducts(null, null));
        }

        [HttpGet]
        [Route("getorders")]
        public async Task<IActionResult> GetOrderLines()
        {
            return Ok(await _orderService.GetOrders(null, "OrderLines"));
        }

        [HttpGet]
        [Route("getjsonreceipts")]
        public async Task<IActionResult> GetJsonReceipts()
        {
            var receipts = await _receiptService.GetReceipts(r => r.ReceiptType == ReceiptType.JSon, null);
            var orderids = receipts.Select(r=>r.OrderId).Distinct();

            var orderViewModels=new List<ReceiptViewModel>();

            foreach (var orderid in orderids)
            {
                var orderReceipt = receipts.FirstOrDefault(r => r.OrderId == orderid);
                orderViewModels.Add(new ReceiptViewModel
                {
                    CompanyName = orderReceipt.CompanyName,
                    OrderId = orderReceipt.OrderId,
                    ReceiptDetails = orderReceipt.ReceiptDetails
                });
            }
           

            return Ok(orderViewModels);
        }

        [HttpGet]
        [Route("gethtmlreceipts")]
        public async Task<IActionResult> GetHtmlReceipts()
        {
            var receipts = await _receiptService.GetReceipts(r => r.ReceiptType == ReceiptType.Html, null);
            var orderids = receipts.Select(r => r.OrderId).Distinct();

            var orderViewModels = new List<ReceiptViewModel>();

            foreach (var orderid in orderids)
            {
                var orderReceipt = receipts.FirstOrDefault(r => r.OrderId == orderid);
                orderViewModels.Add(new ReceiptViewModel
                {
                    CompanyName = orderReceipt.CompanyName,
                    OrderId = orderReceipt.OrderId,
                    ReceiptDetails = orderReceipt.ReceiptDetails
                });
            }


            return Ok(orderViewModels);
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter valid input");
            }
            var productCreatedEvent = await _mediator.Send(model);
            if (productCreatedEvent == null)
            {
                return NotFound();
            }
            if (productCreatedEvent != null)
            {
                await _mediator.Send(new ProductCreated
                {
                    MessageId = productCreatedEvent.MessageId
                });
            }
            var product = new Product
            {
                Id = Guid.NewGuid(),
                HasDisabilityDiscount = productCreatedEvent.HasDisabilityDiscount,
                HasFlatDiscount = productCreatedEvent.HasFlatDiscount,
                HasQuantityDiscount = productCreatedEvent.HasQuantityDiscount,
                Price = productCreatedEvent.Price,
                ProductName = productCreatedEvent.ProductName,
                ProductType = productCreatedEvent.ProductType
            };


            return Ok(product);
        }

    }
}
