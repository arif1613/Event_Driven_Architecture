using System;

namespace OrderService.Data.Models
{
    public class Receipt:BaseDbModel
    {
        public Guid OrderId { get; set; }
        public string ReceiptDetails { get; set; }
        public string CompanyName { get; set; }
        public ReceiptType ReceiptType { get; set; }
        public DateTime ReceiptGenerationTime { get; set; }
    }

    public enum ReceiptType
    {
        JSon=0,
        Html=1
    }
}
