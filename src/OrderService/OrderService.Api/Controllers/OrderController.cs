using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Event;
using OrderService.Api.Model.Notification;
using OrderService.Api.Model.Request;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;


        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrder model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please insert valid data");
            }

            var order = await _mediator.Send(model);
            if (order==null)
            {
                return NotFound("Product(s) is order not found");
            }

            var ordercreatedevent=await _mediator.Send(new OrderCreated
            {
                MessageId = Guid.NewGuid(),
                Order = order
            });




            //Notify email sender service
            await _mediator.Publish(new SendemailNotification
            {
                MessageId = ordercreatedevent.MessageId,
                OrderId = ordercreatedevent.Id,
                CompanyName = order.Company
            });

            return Ok(order);
        }

    }
}
