using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_O_Bot.Resources
{
    public class OrderItemObj
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityString { get; set; }
        public int Quantity { get; set; }
    }
}
