using OrderService.Data.Models;

namespace OrderService.Api.ReceiptGenerator
{
    public interface IOrder
    {
        void AddLine(OrderLine orderLine);
        string GenerateReceipt();
        string GenerateHtmlReceipt();
    }
}