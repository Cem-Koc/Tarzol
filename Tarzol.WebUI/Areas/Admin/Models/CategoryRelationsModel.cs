using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class CategoryRelationsModel
    {
        public Category Category{ get; set; }
        public int SubCategoryID{ get; set; }
        public int CategoryID{ get; set; }
    }
}
