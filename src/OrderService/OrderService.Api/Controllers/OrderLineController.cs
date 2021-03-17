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
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _orderLineService;

        public OrderLineController(IOrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }


        [HttpGet]
        [Route("getorderlines")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _orderLineService.GetOrderLines("Product"));
        }

    }
}
