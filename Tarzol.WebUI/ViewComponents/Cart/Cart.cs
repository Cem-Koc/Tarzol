using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.ViewComponents.Cart
{
    public class Cart : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public Cart(TarzolDbContext tarzolDbContext)
        {

            _tarzolDbContext = tarzolDbContext;
        }
        public IViewComponentResult Invoke()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart==null)
            {
                ViewBag.cartCount = 0;
            }
            else
            {
                ViewBag.cartCount = cart.Count();
            }

            decimal totalPrice = 0;
            decimal totalPriceResult=0;
            if (cart==null)
            {
                ViewBag.cartTotalPrice = totalPrice;
            }
            else if (cart!=null && cart.Count()==0)
            {
                ViewBag.cartTotalPrice = totalPrice;
            }
            else
            {
                foreach (var item in cart)
                {
                    foreach (var product in _tarzolDbContext.Products.Where(i=>i.ID==item.ProductID))
                    {
                        totalPrice = product.DiscountedPrice * item.Quantity;
                        
                        totalPriceResult += totalPrice;
                    }
                    ViewBag.cartTotalPrice = totalPriceResult.ToString("0.00");
                }
            }
            
            
            var productList = _tarzolDbContext.Products.ToList();
            return View(productList);
        }

    }
}
