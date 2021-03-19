using System.ComponentModel.DataAnnotations;
using MediatR;
using OrderService.Api.RequestHandlers;
using OrderService.Data.Models;

namespace OrderService.Api.Model
{
    public class SendEmailRequest : IRequest<EmailSendResult>
    {
        [Required]
        public string EmailBody { get; set; }
    }
}
