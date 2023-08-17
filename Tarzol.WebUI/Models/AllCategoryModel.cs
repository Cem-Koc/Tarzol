using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class AllCategoryModel
    {
        public List<Category> Categories { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
    }
}
