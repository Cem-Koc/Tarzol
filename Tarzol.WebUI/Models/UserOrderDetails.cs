using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class UserOrderDetails
    {
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
