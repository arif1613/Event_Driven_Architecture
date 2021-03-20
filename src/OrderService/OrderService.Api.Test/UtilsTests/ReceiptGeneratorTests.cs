using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Models;

namespace OrderService.Api.Test.UtilsTests
{
    [TestClass]
    public class ReceiptGeneratorTests
    {
        private Order _order;
        [TestInitialize]
        public void Setup()
        {
            var orderlines = Enumerable.Range(0, 1).Select(r => new OrderLine
            {
                Id = Guid.NewGuid(),
                Product = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = $"Insurance Basic",
                    ProductType = $"Car",
                    Price = 1000,
                    HasDisabilityDiscount = true,
                    HasFlatDiscount = true,
                    HasQuantityDiscount = true
                },
                Quantity = 1,
                TotalPrice = 5000
            });

            _order = new Order
            {
                Company = "Test Company",
                OrderLines = orderlines.ToList(),
                Id = Guid.NewGuid()
            };
        }

        [TestMethod]
        public void Can_generate_html_receipt()
        {
            var receiptGenerator=new ReceiptGenerator();
            var htmlReceipt = receiptGenerator.GenerateHtmlReceipt(_order);
            string expected = $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Basic = 1.000,00 kr</li></ul><h3>Subtotal: 1.000,00 kr</h3><h3>MVA: 250,00 kr</h3><h2>Total: 1.250,00 kr</h2></body></html>";
            Assert.IsNotNull(htmlReceipt);
            Assert.AreEqual(htmlReceipt,expected);
        }


        [TestMethod]
        public void Can_generate_json_receipt()
        {
            var receiptGenerator = new ReceiptGenerator();
            var htmlReceipt = receiptGenerator.GenerateJsonReceipt(_order);
            string expected = $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Basic = 1.000,00 kr\r\nSubtotal: 1.000,00 kr\r\nMVA: 250,00 kr\r\nTotal: 1.250,00 kr";
            Assert.IsNotNull(htmlReceipt);
            Assert.AreEqual(htmlReceipt, expected);
        }


    }
}
