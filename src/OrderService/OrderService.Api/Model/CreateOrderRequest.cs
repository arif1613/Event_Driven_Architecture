using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model
{
    public class CreateOrderRequest : IRequest<Order>
    {
        

        [Required]
        public string CompanyName { get; set; }
        [Required]
        public List<CreateProductRequest> Products { get; set; }
    }

    public class CreateProductRequest
    {
        [Required]
        public string ProductType { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
