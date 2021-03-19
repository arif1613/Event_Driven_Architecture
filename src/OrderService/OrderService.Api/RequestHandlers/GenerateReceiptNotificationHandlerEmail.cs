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
    public class GenerateReceiptNotificationHandlerEmail : INotificationHandler<GenerateReceiptNotification>
    {
        private readonly IReceiptGenerator _receiptGenerator;
        private readonly IReceiptService _receiptService;
        private readonly IEmailSender _emailSender;
        private readonly IMediator _mediator;




        public GenerateReceiptNotificationHandlerEmail(IReceiptGenerator receiptGenerator, IReceiptService receiptService, IEmailSender emailSender, IMediator mediator)
        {
            _receiptGenerator = receiptGenerator;
            _receiptService = receiptService;
            _emailSender = emailSender;
            _mediator = mediator;
        }


        public async Task Handle(GenerateReceiptNotification notification, CancellationToken cancellationToken)
        {
            var emailbody = await Task.Run(() => _receiptGenerator.GenerateEmailReceipt(notification.Order));

            if (emailbody == null)
            {
                Console.WriteLine("No email receipt generated");
            }
            else
            {
                var result = await _mediator.Send(new SendEmailRequest
                {
                    EmailBody = emailbody
                });

                if (result == EmailSendResult.Sent)
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


}
