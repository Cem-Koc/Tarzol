using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;
using Tarzol.WebUI.Models;
using Tarzol.DataAccess.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tarzol.WebUI.Controllers
{
    public class CartController : Controller
    {
        IHttpContextAccessor _httpContextAccessor;
        TarzolDbContext _tarzolDbContext;

        public CartController(IHttpContextAccessor httpContextAccessor, TarzolDbContext tarzolDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _tarzolDbContext = tarzolDbContext;
        }

        public IActionResult Index()
        {            
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");           
            ViewBag.cart = cart;

            var productList = _tarzolDbContext.Products.ToList();
            return View(productList);
        }

        private int isExist(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart")==null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { ProductID = id, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index!=-1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { ProductID = id, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        public IActionResult QuantityIncrease(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity++;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }
        public IActionResult QuantityDecrease(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                if (cart[index].Quantity!=1)
                {
                    cart[index].Quantity--;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                
            }
            return RedirectToAction("Index");
        }

        //public IActionResult AddToCart(int Id,string returnUrl)
        //{
        //    var product = _tarzolDbContext.Products.FirstOrDefault(i => i.ID == Id);
        //    if (product != null)
        //    {
        //        GetCart().AddProduct(product, 1);

        //    }
        //    return Redirect(returnUrl);



        //    var cart = new List<CartItem>();
        //    var product = _tarzolDbContext.Products.FirstOrDefault(i => i.ID == Id);
        //    cart.Add(new CartItem()
        //    {
        //        Product = product,
        //        Quantity = 1
        //    });
        //    _httpContextAccessor.HttpContext.Session.SetString("Cart", "cart");
        //    return Redirect(returnUrl);


        //    var product = _tarzolDbContext.Products.FirstOrDefault(i => i.ID == Id);
        //    var cart = SessionHelper.Get<CartItem>(HttpContext.Session,"Cart");
        //    if (cart==null)
        //    {
        //        cart = new CartItem
        //        {
        //            Product=product,
        //            Quantity=1
        //        };

        //        SessionHelper.Set<CartItem>(HttpContext.Session,"Cart", cart);

        //        //SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
        //    }
        //    return Redirect(returnUrl);
        //}


        //public Cart GetCart()
        //{
        //    var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "Cart");
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", "cart");
        //    }

        //    return ;

        //}


        //public IActionResult RemoveFromCart(int Id, string returnUrl)
        //{
        //    var product = _tarzolDbContext.Products.FirstOrDefault(i => i.ID == Id);
        //    if (product != null)
        //    {
        //        GetCart().DeleteProduct(product);
        //    }
        //    return Redirect(returnUrl);
        //}




    }
}
