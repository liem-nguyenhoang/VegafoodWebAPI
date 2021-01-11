using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public int? Calories { get; set; }
        public float? Protein { get; set; }
        public float? Carbs { get; set; }
        public float? Fat { get; set; }
        public byte[] Images { get; set; }
        public string ImagesPath { get; set; }
        public string ProductDescription { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
