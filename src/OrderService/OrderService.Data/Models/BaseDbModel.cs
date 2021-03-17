using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderService.Data.Models
{
    public class BaseDbModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
