using System;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Data.Models
{
    public class BaseDbModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
