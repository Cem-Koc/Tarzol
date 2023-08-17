using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class OrderDetail:BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public bool WasDelivered { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
