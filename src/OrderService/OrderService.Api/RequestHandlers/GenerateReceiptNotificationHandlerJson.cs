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
    public class GenerateReceiptNotificationHandlerJson : INotificationHandler<GenerateJsonReceiptNotification>
    {
        private readonly IReceiptGenerator _receiptGenerator;
        private readonly IReceiptService _receiptService;


        public GenerateReceiptNotificationHandlerJson(IReceiptGenerator receiptGenerator, IReceiptService receiptService)
        {
            _receiptGenerator = receiptGenerator;
            _receiptService = receiptService;
        }


        public async Task Handle(GenerateJsonReceiptNotification notification, CancellationToken cancellationToken)
        {
            var jsonresult = await Task.Run(() => _receiptGenerator.GenerateJsonReceipt(notification.Order), default(CancellationToken));
            if (jsonresult == null)
            {
                Console.WriteLine("No json receipt generated");
            }
            else
            {
                await _receiptService.AddReceipt(new Receipt
                {
                    Id = Guid.NewGuid(),
                    OrderId = notification.Order.Id,
                    CompanyName = notification.Order.Company,
                    ReceiptDetails = jsonresult,
                    ReceiptGenerationTime = DateTime.UtcNow,
                    ReceiptType = ReceiptType.JSon
                });
            }
        }
    }
}
