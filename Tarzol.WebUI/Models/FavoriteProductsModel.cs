using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class FavoriteProductsModel
    {
        public List<Favorite> Favorites{ get; set; }
        public List<Product> Products { get; set; }
    }
}
