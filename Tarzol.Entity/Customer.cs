using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Customer : BaseEntity
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhone { get; set; }
        public int AppUserID { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public int DistrictID { get; set; }



        public virtual District District { get; set; }
        public virtual City City { get; set; }
        
        public virtual AppUser AppUser { get; set; }
        public virtual List<ProductLike> ProductLikes { get; set; }
        public virtual List<Order> Orders { get; set; }
        
    }
}
