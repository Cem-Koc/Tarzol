using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class SellerPageModel
    {
        public Seller Seller{ get; set; }
        public List<Product> Products{ get; set; }
        public List<Product> BestProducts{ get; set; }
    }
}
