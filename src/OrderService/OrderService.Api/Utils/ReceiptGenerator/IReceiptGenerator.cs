using OrderService.Data.Models;

namespace OrderService.Api.Utils.ReceiptGenerator
{
    public interface IReceiptGenerator
    {
        string GenerateJsonReceipt(Order order);
        string GenerateHtmlReceipt(Order order);
    }
}