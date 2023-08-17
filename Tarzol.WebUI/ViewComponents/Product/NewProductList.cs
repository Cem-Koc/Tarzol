using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;

namespace Tarzol.WebUI.ViewComponents.Product
{
    public class NewProductList : ViewComponent
    {
        IProductService _productService;

        public NewProductList(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var result = _productService.GetListAll().Where(x => x.Status == Core.Enums.Status.Active).OrderByDescending(i => i.ID).Take(5).ToList();
            return View(result);
        }
    }
}
