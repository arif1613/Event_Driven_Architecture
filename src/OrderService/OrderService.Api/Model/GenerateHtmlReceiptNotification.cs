using System;
using System.Collections.Generic;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model
{
    public class GenerateHtmlReceiptNotification : ICustomNotification
    {
        public Order Order { get; set; }

        public Guid MessageId { get; set; }

        public GenerateHtmlReceiptNotification()
        {
            MessageId=Guid.NewGuid();
        }
    }
}
