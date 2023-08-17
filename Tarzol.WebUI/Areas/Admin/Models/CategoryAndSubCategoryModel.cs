using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class CategoryAndSubCategoryModel
    {
        public List<Category> Categories{ get; set; }
        public List<CategoryAndSubCategory> CategoryAndSubCategories { get; set; }
    }
}
