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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getproducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProducts());
        }

    }
}
