using System.ComponentModel.DataAnnotations;
using MediatR;
using OrderService.Data.Models;

namespace OrderService.Api.Model.Request
{
    public class CreateProduct:IRequest<ProductCreatedEvent>
    {
        [Required]
        public string ProductType { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }

        public bool HasQuantityDiscount { get; set; }
        public bool HasDisabilityDiscount { get; set; }
        public bool HasFlatDiscount { get; set; }
    }
}
