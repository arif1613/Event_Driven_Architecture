using System.ComponentModel.DataAnnotations;

namespace OrderService.Data.Models
{
	public class Product: BaseDbModel
	{
        [Required]
		public string ProductType { get; set; }
        [Required]
		public string ProductName { get; set; }
        public int Price { get; set; }

        public bool HasQuantityDiscount { get; set; }
        public bool HasDisabilityDiscount { get; set; }
        public bool HasFlatDiscount { get; set; }
	}
}