using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model.Notification;
using OrderService.Api.Services;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class GenerateReceiptNotificationHandlerEmail : INotificationHandler<SendemailNotification>
    {
        private readonly IReceiptService _receiptService;
        private readonly IOrderEventService _orderEventService;


        public GenerateReceiptNotificationHandlerEmail(IReceiptService receiptService, IOrderEventService orderEventService)
        {
            _receiptService = receiptService;
            _orderEventService = orderEventService;
        }
        public async Task Handle(SendemailNotification notification, CancellationToken cancellationToken)
        {
            var orderFromEventStore =
                await _orderEventService.GetEventStoreOrder(r => r.MessageId == notification.MessageId,null);

            if (orderFromEventStore == null)
            {
                Console.WriteLine("No order found in event store");
            }
            else
            {
                    await _receiptService.AddReceipt(new Receipt
                    {
                        Id = Guid.NewGuid(),
                        OrderId = notification.OrderId,
                        ReceiptDetails = orderFromEventStore.HtmlReceipt,
                        CompanyName = notification.CompanyName,
                        ReceiptGenerationTime = DateTime.UtcNow,
                        ReceiptType = ReceiptType.Html
                    });

                    await _receiptService.AddReceipt(new Receipt
                    {
                        Id = Guid.NewGuid(),
                        OrderId = notification.OrderId,
                        ReceiptDetails = orderFromEventStore.JsonReceipt,
                        ReceiptGenerationTime = DateTime.UtcNow,
                        CompanyName = notification.CompanyName,
                        ReceiptType = ReceiptType.JSon
                    });
            }
        }

       
    }


}
