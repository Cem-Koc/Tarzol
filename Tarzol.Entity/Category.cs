using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

       


        public virtual List<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
        public virtual List<Product> Products { get; set; }
        
    }
}
