using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class SizeListAndNewSizeModel
    {
        public List<ProductSize> ProductSizes{ get; set; }
        public ProductSize ProductSize{ get; set; }
    }
}
