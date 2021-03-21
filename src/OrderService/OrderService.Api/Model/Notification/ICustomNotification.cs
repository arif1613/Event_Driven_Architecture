using System;
using MediatR;

namespace OrderService.Api.Model.Notification
{
    public interface ICustomNotification:INotification
    {
        public Guid MessageId { get; set; }
    }
}
