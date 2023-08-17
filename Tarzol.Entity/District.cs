using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class District : BaseEntity
    {
        public string DistrictName { get; set; }
        public int CityID { get; set; }

        public virtual City City { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Seller> Sellers { get; set; }
    }
}
