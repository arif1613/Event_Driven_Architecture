using System.ComponentModel.DataAnnotations;

namespace OrderService.Data.Models
{
    public class Pin
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(1000,9999)]
        public int Value { get; set; }

        [Required]
        public bool IsUsed { get; set; }
    }
}
