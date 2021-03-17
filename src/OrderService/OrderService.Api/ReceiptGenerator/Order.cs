using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderService.Api.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.ReceiptGenerator
{
	public class Order:IOrder
	{
		private readonly IList<OrderLine> _orderLines = new List<OrderLine>();
        private string Company { get; }
		public Order(string company)
		{
			Company = company;
		}


		public void AddLine(OrderLine orderLine)
		{
			_orderLines.Add(orderLine);
		}

		public string GenerateReceipt()
		{
			double totalAmount = 0d;
			StringBuilder result = new StringBuilder($"Order receipt for '{Company}'{Environment.NewLine}");
			foreach (OrderLine line in _orderLines)
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

		public string GenerateHtmlReceipt()
		{
			double totalAmount = 0d;
			StringBuilder result = new StringBuilder($"<html><body><h1>Order receipt for '{Company}'</h1>");
			if (_orderLines.Any())
			{
				result.Append("<ul>");
				foreach (OrderLine line in _orderLines)
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
	}
}