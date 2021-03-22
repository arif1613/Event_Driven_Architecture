using System;

namespace OrderService.Api.Model.ViewModel
{
    public class ReceiptViewModel
    {
        public Guid OrderId { get; set; }
        public string ReceiptDetails { get; set; }
        public string CompanyName { get; set; }
    }
}
