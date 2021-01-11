using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipDistrict { get; set; }
        public string Description { get; set; }
        public int? PaymentVia { get; set; }
        public bool? OrderStatus { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Payments PaymentViaNavigation { get; set; }
        public virtual Shippers ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
