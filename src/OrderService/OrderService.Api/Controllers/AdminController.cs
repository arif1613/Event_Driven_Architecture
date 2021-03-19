using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Data.Models;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IReceiptService _receiptService;
        private readonly IOrderService _orderService;



        public AdminController(IProductService productService, IReceiptService receiptService, IOrderService orderService)
        {
            _productService = productService;
            _receiptService = receiptService;
            _orderService = orderService;
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
            return Ok(await _receiptService.GetReceipts(r=>r.ReceiptType==ReceiptType.JSon, null));
        }

        [HttpGet]
        [Route("gethtmlreceipts")]
        public async Task<IActionResult> GetHtmlReceipts()
        {
            return Ok(await _receiptService.GetReceipts(r => r.ReceiptType == ReceiptType.Html, null));
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter valid input");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = model.ProductName,
                ProductType = model.ProductType,
                Price = model.Price,
                HasDisabilityDiscount = model.HasDisabilityDiscount,
                HasFlatDiscount = model.HasFlatDiscount,
                HasQuantityDiscount = model.HasQuantityDiscount
            };
            await _productService.AddProduct(product);
            return Ok(product);
        }

    }
}
