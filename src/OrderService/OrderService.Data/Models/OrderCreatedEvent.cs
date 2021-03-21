using System;

namespace OrderService.Data.Models
{
    public class OrderCreatedEvent:BaseDbModel
    {
        public Guid MessageId { get; set; }
        public string JsonReceipt { get; set; }
        public string HtmlReceipt { get; set; }

    }
}
