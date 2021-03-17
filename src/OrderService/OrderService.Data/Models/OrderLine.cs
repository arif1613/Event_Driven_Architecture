namespace OrderService.Data.Models
{
	public class OrderLine: BaseDbModel
	{
        public virtual Product Product { get; set; }
		public int Quantity { get; set; }
	}
}