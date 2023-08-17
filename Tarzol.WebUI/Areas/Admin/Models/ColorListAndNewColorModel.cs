using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class ColorListAndNewColorModel
    {
        public List<ProductColor> ProductColors{ get; set; }
        public ProductColor ProductColor { get; set; }
    }
}
