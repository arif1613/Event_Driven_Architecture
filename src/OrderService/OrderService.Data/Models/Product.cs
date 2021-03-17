namespace OrderService.Data.Models
{
	public class Product: BaseDbModel
	{
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
		
	}
}