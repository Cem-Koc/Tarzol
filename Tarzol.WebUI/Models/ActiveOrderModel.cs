using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class ActiveOrderModel
    {
        public List<Product>Products{ get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
