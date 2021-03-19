using System.ComponentModel.DataAnnotations;

namespace OrderService.Api.Model
{
    public class AddProduct
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
