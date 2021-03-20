using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class GenerateReceiptNotificationHandlerHtml : INotificationHandler<GenerateHtmlReceiptNotification>
    {
        private readonly IReceiptGenerator _receiptGenerator;
        private readonly IReceiptService _receiptService;


        public GenerateReceiptNotificationHandlerHtml(IReceiptGenerator receiptGenerator, IReceiptService receiptService)
        {
            _receiptGenerator = receiptGenerator;
            _receiptService = receiptService;
        }
        public async Task Handle(GenerateHtmlReceiptNotification notification, CancellationToken cancellationToken)
        {
            var htmlresult = await Task.Run(() => _receiptGenerator.GenerateHtmlReceipt(notification.Order), default(CancellationToken));
            if (htmlresult == null)
            {
                Console.WriteLine("No html receipt generated");
            }
            else
            {
                await _receiptService.AddReceipt(new Receipt
                {
                    Id = Guid.NewGuid(),
                    OrderId = notification.Order.Id,
                    CompanyName = notification.Order.Company,
                    ReceiptDetails = htmlresult,
                    ReceiptGenerationTime = DateTime.UtcNow,
                    ReceiptType = ReceiptType.Html
                });
            }
        }
    }
}
