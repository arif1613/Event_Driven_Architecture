using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Data.Models;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;


        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please insert valid data");
            }

            var order = await _mediator.Send(model,default(CancellationToken));
            if (order==null)
            {
                return NotFound("Product not found");
            }
            await _mediator.Publish(new GenerateReceiptNotification
            {
                Order = order
            }, default(CancellationToken));

            return Ok(order);
        }

    }
}
