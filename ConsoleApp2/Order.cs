using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Order
    {
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }

        public class OrderItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
