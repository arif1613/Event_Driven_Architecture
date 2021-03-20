using System;
using System.Collections.Generic;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model
{
    public class GenerateJsonReceiptNotification : ICustomNotification
    {
        public Order Order { get; set; }
        public Guid MessageId { get; set; }

        public GenerateJsonReceiptNotification()
        {
            MessageId = Guid.NewGuid();
        }

    }
}
