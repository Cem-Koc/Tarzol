using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;

namespace Tarzol.WebUI.ViewComponents.Product
{
    public class BestSellers3 : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public BestSellers3(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var products = _tarzolDbContext.OrderDetails.Select(i => i.ProductID).Distinct().Take(3).ToList();
            var productList = new List<Tarzol.Entity.Product>();
            foreach (var item in products)
            {
                var product = _tarzolDbContext.Products.Where(i => i.ID == item).FirstOrDefault();
                productList.Add(product);
            }
            return View(productList);
        }
    }
}
