using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class SalesTotalsByAmount
    {
        public decimal? SaleAmount { get; set; }
        public int OrderId { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
