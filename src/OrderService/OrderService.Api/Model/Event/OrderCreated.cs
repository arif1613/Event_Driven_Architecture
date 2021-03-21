using System;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model.Event
{
    public class OrderCreated : IRequest<OrderCreatedEvent>
    {
        public Guid MessageId { get; set; }
        public Order Order { get; set; }

    }
}
