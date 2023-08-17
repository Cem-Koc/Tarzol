using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var productList = _productService.GetListAll();
            return View(productList);
        }

        public IActionResult ProductDetail(int productId,bool notificationPage,int productRatingId)
        {
            if (notificationPage==true)
            {
                ViewBag.notificationPage = true;
            }
            if (productRatingId!=0)
            {
                var productRating = _tarzolDbContext.ProductRatings.Where(i => i.ID == productRatingId).FirstOrDefault();
                productId = productRating.ProductID.Value;
                var orderId = productRating.OrderID;
                var userId = productRating.AppUserID;
                var comment = _tarzolDbContext.Comments.Where(i => i.AppUserID == userId).Where(i => i.OrderID == orderId).Where(i => i.ProductID == productId).FirstOrDefault();
                ViewBag.commentContext = comment.CommentContext;
                ViewBag.comment = comment.ID;
                var user = _tarzolDbContext.Users.Where(i => i.Id == comment.AppUserID).FirstOrDefault();
                var username = user.FirstName + " " + user.LastName;
                ViewBag.commentUser = username;
            }
            var product = _productService.GetBy(productId);
            return View(product);
        }

        [HttpGet]
        public IActionResult ProductEdit(int productId)
        {
            ViewBag.Color = new SelectList(_tarzolDbContext.ProductColors, "ID", "Color");
            ViewBag.Size = new SelectList(_tarzolDbContext.ProductSizes, "ID", "Size");

            var product = _productService.GetBy(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProductColor()
        {
            var productColor = _tarzolDbContext.ProductColors.ToList();
            ColorListAndNewColorModel colorListAndNewColorModel = new ColorListAndNewColorModel();
            colorListAndNewColorModel.ProductColors = productColor;
            return View(colorListAndNewColorModel);
        }
        [HttpPost]
        public IActionResult ProductColor(ProductColor productColor)
        {
            
            productColor.Status = Core.Enums.Status.Active;
            var upperColor=productColor.Color.ToUpper();
            productColor.Color = upperColor;
            _tarzolDbContext.ProductColors.Add(productColor);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("ProductColor");
        }

        public IActionResult ProductColorDelete(int productColorId)
        {
            var productColor = _tarzolDbContext.ProductColors.Where(i => i.ID == productColorId).FirstOrDefault();
            _tarzolDbContext.ProductColors.Remove(productColor);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("ProductColor");
        }
        [HttpGet]
        public IActionResult ProductSize()
        {
            var productSize = _tarzolDbContext.ProductSizes.ToList();
            SizeListAndNewSizeModel sizeListAndNewSizeModel = new SizeListAndNewSizeModel();
            sizeListAndNewSizeModel.ProductSizes = productSize;
            return View(sizeListAndNewSizeModel);
        }
        [HttpPost]
        public IActionResult ProductSize(ProductSize productSize)
        {

            productSize.Status = Core.Enums.Status.Active;
            var upperColor = productSize.Size.ToUpper();
            productSize.Size = upperColor;
            _tarzolDbContext.ProductSizes.Add(productSize);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("ProductSize");
        }

        public IActionResult ProductSizeDelete(int productSizeId)
        {
            var productSize = _tarzolDbContext.ProductSizes.Where(i => i.ID == productSizeId).FirstOrDefault();
            _tarzolDbContext.ProductSizes.Remove(productSize);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("ProductSize");
        }
    }
}
