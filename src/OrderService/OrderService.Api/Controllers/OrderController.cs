using System;
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

            var order = await _mediator.Send(model);
            if (order==null)
            {
                return NotFound("Product not found");
            }


            //Notify json generator service
            await _mediator.Publish(new GenerateJsonReceiptNotification
            {
                MessageId = Guid.NewGuid(),
                Order = order
            });

            //Notify html generator service
            await _mediator.Publish(new GenerateHtmlReceiptNotification
            {
                MessageId = Guid.NewGuid(),
                Order = order
            });

            //Notify email sender service
            await _mediator.Publish(new SendemailNotification
            {
                MessageId = Guid.NewGuid(),
                Order = order
            });

            return Ok(order);
        }

    }
}
