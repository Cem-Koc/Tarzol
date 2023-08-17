using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
   public class Seller : BaseEntity
    {
        public int UserID { get; set; }
        public string CompanyName { get; set; }        
        public string SellerAddress { get; set; }
        public string TaxNumber { get; set; }       
        public string ProfileImageUrl { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }


        public virtual City City { get; set; }
        public virtual District District { get; set; }
       
        public virtual List<Product> Product { get; set; }
        public virtual List<Answer> Answers { get; set; }
       
    }
}
