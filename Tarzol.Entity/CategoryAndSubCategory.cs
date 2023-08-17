using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class CategoryAndSubCategory
    {
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
