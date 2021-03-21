using System;
using MediatR;

namespace OrderService.Api.Model.Event
{
    public class ProductCreated:IRequest<Unit>
    {
        public Guid MessageId { get; set; }
    }
}
