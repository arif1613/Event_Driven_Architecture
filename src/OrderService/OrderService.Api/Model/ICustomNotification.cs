using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace OrderService.Api.Model
{
    public interface ICustomNotification:INotification
    {
        public Guid MessageId { get; set; }
    }
}
