using System.Collections.Generic;

namespace CSGOMarket.Models
{
    public class OrderList
    {
        public bool Success { get; set; }

        public List<Order> Orders { get; set; }
    }
}
