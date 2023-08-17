using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class ProductSize : BaseEntity
    {
        public string Size { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
