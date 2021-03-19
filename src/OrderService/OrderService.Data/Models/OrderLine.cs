using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Data.Models
{
    public class OrderLine : BaseDbModel
    {
        public virtual Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}