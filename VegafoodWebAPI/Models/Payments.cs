using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class Payments
    {
        public Payments()
        {
            Orders = new HashSet<Orders>();
        }

        public int PaymentId { get; set; }
        public string PaymentSolution { get; set; }
        public string PayCompany { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
