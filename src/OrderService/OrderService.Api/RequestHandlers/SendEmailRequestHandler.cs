using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Api.Model;
using OrderService.Api.Services;
using OrderService.Api.Utils.EmailSender;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.RequestHandlers
{
    public class SendEmailRequestHandler : IRequestHandler<SendEmailRequest, EmailSendResult>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailRequestHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }


        public async Task<EmailSendResult> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _emailSender.SendEmail(request.EmailBody);
                return EmailSendResult.Sent;
            }
            catch (Exception e)
            {
                return EmailSendResult.Error;
            }
        }
    }

    public enum EmailSendResult
    {
        Sent=0,
        Error=1
    }
}
