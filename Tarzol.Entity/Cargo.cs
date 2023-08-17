using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Cargo : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
