using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class ProductAddViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
        public Product Product { get; set; }
    }
}
