using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.ViewComponents.Seller
{
    public class FeaturedSeller3 : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public FeaturedSeller3(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var sellersId = _tarzolDbContext.Products.Select(i => i.SellerID).Distinct().Take(3).ToList();
            var sellerList = _tarzolDbContext.Sellers.ToList();
            List<Tarzol.Entity.Seller> sellers = new List<Entity.Seller>();
            foreach (var item in sellersId)
            {
                var seller = _tarzolDbContext.Sellers.Where(i => i.ID == item).FirstOrDefault();
                sellers.Add(seller);
            }

            var newList = sellerList.Except(sellers);
            if (sellers.Count<3)
            {
                foreach (var item in newList)
                {
                    if (sellers.Count<3)
                    {
                        sellers.Add(item);
                    }
                }
            }
            return View(sellers);
        }
    }
}
