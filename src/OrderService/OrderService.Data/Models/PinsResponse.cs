using System.Collections.Generic;

namespace OrderService.Data.Models
{
    public class PinsResponse
    {
        public int Count { get; set; }
        public List<Pin> Pins { get; set; }

    }
}
