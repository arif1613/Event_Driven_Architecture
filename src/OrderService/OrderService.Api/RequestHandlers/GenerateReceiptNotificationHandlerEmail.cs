using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Api.Utils.EmailSender;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class GenerateReceiptNotificationHandlerEmail : INotificationHandler<SendemailNotification>
    {
        private readonly IReceiptGenerator _receiptGenerator;
        private readonly IReceiptService _receiptService;

        public GenerateReceiptNotificationHandlerEmail(IReceiptGenerator receiptGenerator, IReceiptService receiptService)
        {
            _receiptGenerator = receiptGenerator;
            _receiptService = receiptService;
        }
        public async Task Handle(SendemailNotification notification, CancellationToken cancellationToken)
        {
            var emailbody = await Task.Run(() => _receiptGenerator.GenerateEmailReceipt(notification.Order),default(CancellationToken));

            if (emailbody == null)
            {
                Console.WriteLine("No email receipt generated");
            }
            else
            {
                    await _receiptService.AddReceipt(new Receipt
                    {
                        Id = Guid.NewGuid(),
                        OrderId = notification.Order.Id,
                        CompanyName = notification.Order.Company,
                        ReceiptDetails = emailbody,
                        ReceiptGenerationTime = DateTime.UtcNow,
                        ReceiptType = ReceiptType.email
                    });
            }
        }

       
    }


}
