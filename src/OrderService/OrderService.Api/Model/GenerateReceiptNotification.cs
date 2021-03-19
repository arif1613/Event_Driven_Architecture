using System.Collections.Generic;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model
{
    public class GenerateReceiptNotification : INotification
    {
        public Order Order { get; set; }

    }
}
