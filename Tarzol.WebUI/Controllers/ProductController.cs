using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class ProductController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IProductService _productService;
        

        public ProductController(TarzolDbContext tarzolDbContext, IProductService productService)
        {
            _tarzolDbContext = tarzolDbContext;
            _productService = productService;
            
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ViewProduct(int id)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            if (user!=null)
            {
                var favorites = _tarzolDbContext.Favorites.Where(i => i.AppUserID == user.Id).Select(i=>i.ProductID).Distinct().ToList();   
                
                ViewBag.userFavorites = favorites;
                
            }

            if (id!=0)
            {
                var productRatingCount = _tarzolDbContext.ProductRatings.Where(i => i.ProductID == id).Count();
                if (productRatingCount!=0)
                {
                    var productRatingValue = _tarzolDbContext.ProductRatings.Where(i => i.ProductID == id).Select(x => x.ProductRatingPoint).Average();
                    var productRating = Math.Ceiling((decimal)productRatingValue);
                    ViewBag.productRating = productRating;

                }

                ViewBag.productRatingCount = productRatingCount;
            }
           


            ViewBag.productid = id;
            var values = _productService.GetProductByID(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {
            ViewBag.Cat = new SelectList(_tarzolDbContext.Categories, "ID", "CategoryName");
            


            return View();
          
        }
        //public JsonResult LoadSubProperty(int Id)
        //{
        //    var subProperty = _tarzolDbContext.ProductSubProperties.Where(x => x.ProductPropertyID == Id).ToList();
        //    return Json(new SelectList(subProperty, "ID", "ProductSubPropertyName"));
        //}

        //public JsonResult LoadSubCategory(int Id)
        //{
        //    var subCategory = _tarzolDbContext.SubCategories.Where(x => x.CategoryID == Id).ToList();
        //    return Json(new SelectList(subCategory, "ID", "SubCategoryName"));
        //}

        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {
            product.Status = Core.Enums.Status.Active;
            product.SellerID = 1;
            product.CargoID = 1;
            _productService.Add(product);
            _tarzolDbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult ProductAddTwoPage(Product product)
        {

            Product p = new Product();
            p.ProductName = product.ProductName;
            p.TradeMark = product.TradeMark;
            p.ProductCode = product.ProductCode;
            p.MarketPrice = product.MarketPrice;
            p.DiscountedPrice = product.DiscountedPrice;
            p.UnitsInStock = product.UnitsInStock;
            p.CategoryID = product.CategoryID;
            
            ViewBag.prop = p;

            var catandsubcat = _tarzolDbContext.CategoryAndSubCategories.Include("SubCategory").Where(i => i.CategoryID == product.CategoryID).ToList();          
           
            List<SubCategory> substop = new List<SubCategory>();

            int[] subcatid = new int[_tarzolDbContext.SubCategories.Count()];
            substop = _tarzolDbContext.SubCategories.ToList();
            int subidcount = 0;
            foreach (var item in substop)
            {
                subcatid[subidcount] = item.ID;
                subidcount = subidcount + 1;
            }

            SubCategory[] koc = new SubCategory[catandsubcat.Count()];
            int kocc = 0;

            foreach (var salmon in catandsubcat)
            {
                var coc = _tarzolDbContext.SubCategories.Where(i => i.ID == salmon.SubCategoryID).FirstOrDefault();
                
                koc[kocc] = coc;
                kocc = kocc + 1;
            }
            
            ViewBag.SubCat = new SelectList(koc, "ID", "SubCategoryName");


            
            






            return View();
        }
       

        //////////////////////////////////////////////////////

        [HttpGet]
        public IActionResult ProductEkle()
        {
           
            return View();
        }
    }
}
