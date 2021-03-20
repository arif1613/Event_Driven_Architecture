using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderService.Data.Models;

namespace OrderService.Api.Utils.ReceiptGenerator
{
    public class ReceiptGenerator : IReceiptGenerator
    {

        public string GenerateJsonReceipt(Order order)
        {
            double totalAmount = 0d;
            StringBuilder result = new StringBuilder($"Order receipt for '{order.Company}'{Environment.NewLine}");
            foreach (OrderLine line in order.OrderLines)
            {
                double thisAmount = 0d;
                switch (line.Product.Price)
                {
                    case Prices.OneThousand:
                        if (line.Quantity >= 5)
                            thisAmount += line.Quantity * line.Product.Price * .9d;
                        else
                            thisAmount += line.Quantity * line.Product.Price;
                        break;
                    case Prices.TwoThousand:
                        if (line.Quantity >= 3)
                            thisAmount += line.Quantity * line.Product.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Product.Price;
                        break;
                    default:
                        thisAmount = line.Quantity * line.Product.Price;
                        break;
                }

                result.AppendLine($"\t{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}");
                totalAmount += thisAmount;
            }

            result.AppendLine($"Subtotal: {totalAmount:C}");
            double totalTax = totalAmount * Prices.TaxRate;
            result.AppendLine($"MVA: {totalTax:C}");
            result.Append($"Total: {totalAmount + totalTax:C}");
            return result.ToString();
        }

        public string GenerateHtmlReceipt(Order order)
        {
            double totalAmount = 0d;
            StringBuilder result = new StringBuilder($"<html><body><h1>Order receipt for '{order.Company}'</h1>");
            if (order.OrderLines.Any())
            {
                result.Append("<ul>");
                foreach (OrderLine line in order.OrderLines)
                {
                    double thisAmount = 0d;
                    switch (line.Product.Price)
                    {
                        case Prices.OneThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity * line.Product.Price * .9d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                        case Prices.TwoThousand:
                            if (line.Quantity >= 3)
                                thisAmount += line.Quantity * line.Product.Price * .8d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                        default:
                            thisAmount = line.Quantity * line.Product.Price;
                            break;
                    }

                    result.Append($"<li>{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}</li>");
                    totalAmount += thisAmount;
                }

                result.Append("</ul>");
            }

            result.Append($"<h3>Subtotal: {totalAmount:C}</h3>");
            double totalTax = totalAmount * Prices.TaxRate;
            result.Append($"<h3>MVA: {totalTax:C}</h3>");
            result.Append($"<h2>Total: {totalAmount + totalTax:C}</h2>");
            result.Append("</body></html>");
            return result.ToString();
        }

        public string GenerateEmailReceipt(Order order)
        {
            double totalAmount = 0d;
            StringBuilder result = new StringBuilder($"Order receipt for '{order.Company}'{Environment.NewLine}");
            if (order.OrderLines.Any())
            {

                foreach (OrderLine line in order.OrderLines)
                {
                    double thisAmount = 0d;
                    switch (line.Product.Price)
                    {
                        case Prices.OneThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity * line.Product.Price * .9d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                        case Prices.TwoThousand:
                            if (line.Quantity >= 3)
                                thisAmount += line.Quantity * line.Product.Price * .8d;
                            else
                                thisAmount += line.Quantity * line.Product.Price;
                            break;
                        default:
                            thisAmount = line.Quantity * line.Product.Price;
                            break;
                    }

                    result.AppendLine($"\t{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}");
                    totalAmount += thisAmount;
                }
            }
            result.AppendLine($"Subtotal: {totalAmount:C}");
            double totalTax = totalAmount * Prices.TaxRate;
            result.AppendLine($"MVA: {totalTax:C}");
            result.Append($"Total: {totalAmount + totalTax:C}");
            return result.ToString();
        }
    }
}