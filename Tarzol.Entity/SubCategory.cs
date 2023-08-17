using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class SubCategory : BaseEntity
    {
        public string SubCategoryName { get; set; }

        public virtual List<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
