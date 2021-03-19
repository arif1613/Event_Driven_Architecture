﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.Data.Models
{
    public class Order:BaseDbModel
    {
        public string Company { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
