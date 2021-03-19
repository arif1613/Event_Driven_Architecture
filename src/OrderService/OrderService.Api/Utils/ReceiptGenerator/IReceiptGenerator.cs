using OrderService.Data.Models;

namespace OrderService.Api.Utils.ReceiptGenerator
{
    public interface IReceiptGenerator
    {
        string GenerateReceipt(Order order);
        string GenerateHtmlReceipt(Order order);
        string GenerateEmailReceipt(Order order);

    }
}