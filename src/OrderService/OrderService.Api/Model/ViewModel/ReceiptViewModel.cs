using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Api.Model.ViewModel
{
    public class ReceiptViewModel
    {
        public Guid OrderId { get; set; }
        public string ReceiptDetails { get; set; }
        public string CompanyName { get; set; }
    }
}
