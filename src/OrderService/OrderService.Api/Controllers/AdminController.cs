using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Api.Services;
using OrderService.Api.UnitOfWork;
using OrderService.Data.Models;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderLineService _orderlineService;


        public AdminController(IProductService productService, IOrderLineService orderlineService)
        {
            _productService = productService;
            _orderlineService = orderlineService;
        }

        [HttpPost]
        [Route("bulkinsertorderlines")]
        public async Task<IActionResult> BulkInsertOrderLines([FromBody] int totalrecord)
        {
            var orderLines = Enumerable.Range(0, totalrecord).Select(r => new OrderLine
            {
                Id = Guid.NewGuid(),
                Product = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = $"test product name {r}",
                    ProductType = $"test product type {r}",
                    Price = r * 10
                }
            });
            try
            {
                await _orderlineService.AddOrderLines(orderLines.ToList());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return Ok($"{totalrecord} products added");
        }
    }
}
