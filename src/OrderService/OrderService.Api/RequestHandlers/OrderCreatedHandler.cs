using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Event;
using OrderService.Api.Services;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class OrderCreatedHandler : IRequestHandler<OrderCreated, OrderCreatedEvent>
    {
        private readonly IReceiptGenerator _receiptGenerator;
        private readonly IOrderEventService _orderEventService;


        public OrderCreatedHandler(IReceiptGenerator receiptGenerator, IOrderEventService orderEventService)
        {
            _receiptGenerator = receiptGenerator;
            _orderEventService = orderEventService;
        }


        public async Task<OrderCreatedEvent> Handle(OrderCreated request, CancellationToken cancellationToken)
        {
            var jsonreceipt = _receiptGenerator.GenerateJsonReceipt(request.Order);
            var htmlreceipt = _receiptGenerator.GenerateHtmlReceipt(request.Order);
            var ordercreatedevent = new OrderCreatedEvent
            {
                HtmlReceipt = htmlreceipt,
                JsonReceipt = jsonreceipt,
                MessageId = request.MessageId,
                Id = Guid.NewGuid()
            };
            await _orderEventService.AddEvent(ordercreatedevent);
            return ordercreatedevent;
        }
    }
}
