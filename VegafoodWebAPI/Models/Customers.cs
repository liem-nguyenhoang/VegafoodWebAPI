using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public string CustomerId { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string District { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
