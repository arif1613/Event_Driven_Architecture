namespace OrderService.Data.Models
{
	public class Product
	{
        public string ProductType { get; }
        public string ProductName { get; }
        public int Price { get; }
		public Product(string productType, string productName, int price)
		{
			ProductType = productType;
			ProductName = productName;
			Price = price;
		}
		
	}
}