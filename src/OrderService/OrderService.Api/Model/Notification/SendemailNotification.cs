using System;

namespace OrderService.Api.Model.Notification
{
    public class SendemailNotification : ICustomNotification
    {
        public Guid MessageId { get; set; }
        public Guid OrderId { get; set; }
        public string CompanyName { get; set; }


    }
}
