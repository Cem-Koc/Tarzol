using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
   public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public DateTime? EstimatedDelivreyDate { get; set; }
        public DateTime? DelivreyDate { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
       
        public int CustomerID { get; set; }




        public virtual Customer Customer { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<ProductRating> ProductRatings { get; set; }
    }
}
