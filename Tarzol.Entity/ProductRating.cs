using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class ProductRating : BaseEntity
    {
        public int ProductRatingPoint { get; set; }
        public int? ProductID { get; set; }
        public int? OrderID { get; set; }
        public int? AppUserID { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Order Order { get; set; }
    }
}
