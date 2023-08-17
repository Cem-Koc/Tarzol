using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Tarzol.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.ViewComponents.Product
{
    public class FeaturedProductList : ViewComponent
    {
        IProductService _productService;
        TarzolDbContext _tarzolDbContext;

        public FeaturedProductList(IProductService productService,TarzolDbContext tarzolDbContext)
        {
            _productService = productService;
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var featuredProduct = _tarzolDbContext.OrderDetails.Select(i => i.ProductID).Distinct().Take(5).ToList();
            List<Tarzol.Entity.Product> products = new List<Entity.Product>();
            foreach (var productId in featuredProduct)
            {
                var product = _productService.GetBy(productId);
                if (product.Status==Core.Enums.Status.Active)
                {
                    products.Add(product);
                }
            }

            if (products.Count<5)
            {
                var result = _productService.GetListAll().Where(x => x.Status == Core.Enums.Status.Active).OrderBy(i => i.ID).ToList();
                var productList = result.Except(products);
                foreach (var item in productList)
                {
                    if (products.Count<5)
                    {
                        products.Add(item);
                    }
                }
            }
            return View(products);
        }
    }
}
