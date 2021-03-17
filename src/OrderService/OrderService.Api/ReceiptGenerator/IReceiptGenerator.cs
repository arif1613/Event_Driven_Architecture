using OrderService.Data.Models;

namespace OrderService.Api.ReceiptGenerator
{
    public interface IReceiptGenerator
    {
        void AddLine(OrderLine orderLine);
        string GenerateReceipt();
        string GenerateHtmlReceipt();
    }
}