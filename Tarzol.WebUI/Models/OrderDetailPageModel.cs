using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class OrderDetailPageModel
    {
        public Order Order { get; set; }
        public List<Product>Products { get; set; }
        public List<OrderDetail>orderDetails { get; set; }
        public Customer Customer { get; set; }
    }
}
