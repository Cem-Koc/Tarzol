using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.AutoMapperTier.ViewModels;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;


namespace Tarzol.WebUI.Controllers
{
    public class TestController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IMapper _mapper;
        IProductService _productService;

        public TestController(TarzolDbContext tarzolDbContext,IMapper mapper, IProductService productService)
        {
            _tarzolDbContext = tarzolDbContext;
            _mapper = mapper;
            _productService = productService;
           
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Kategori = new SelectList(_tarzolDbContext.Categories, "ID", "CategoryName");
            ViewBag.Color = new SelectList(_tarzolDbContext.ProductColors, "ID", "Color");
            ViewBag.Size = new SelectList(_tarzolDbContext.ProductSizes, "ID", "Size");
            return View();
        }

        [HttpPost]
        public IActionResult Index(ProductWithImageAdd p)
        {
            Product product = new Product();
            if (p.ImageOne != null)
            {
                var extension = Path.GetExtension(p.ImageOne.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageOne.CopyTo(stream);
                product.ImageOne = newimagename;

                if (p.ImageTwo != null)
                {
                    var extension2 = Path.GetExtension(p.ImageTwo.FileName);
                    var newimagename2 = Guid.NewGuid() + extension2;
                    var location2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename2);
                    var stream2 = new FileStream(location2, FileMode.Create);
                    p.ImageTwo.CopyTo(stream2);
                    product.ImageTwo = newimagename2;
                }
                if (p.ImageThree != null)
                {
                    var extension3 = Path.GetExtension(p.ImageThree.FileName);
                    var newimagename3 = Guid.NewGuid() + extension3;
                    var location3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename3);
                    var stream3 = new FileStream(location3, FileMode.Create);
                    p.ImageThree.CopyTo(stream3);
                    product.ImageThree = newimagename3;
                }
                if (p.ImageFour != null)
                {
                    var extension4 = Path.GetExtension(p.ImageFour.FileName);
                    var newimagename4 = Guid.NewGuid() + extension4;
                    var location4 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename4);
                    var stream4 = new FileStream(location4, FileMode.Create);
                    p.ImageFour.CopyTo(stream4);
                    product.ImageFour = newimagename4;
                }
            }

            product.ProductName = p.ProductName;
            product.MarketPrice = p.MarketPrice;
            product.UnitsInStock = p.UnitsInStock;
            product.DiscountedPrice = p.DiscountedPrice;
            product.TradeMark = p.TradeMark;
            product.ProductCode = p.ProductCode;
            product.CategoryID = p.CategoryID;
            product.SubCategoryID = p.SubCategoryID;
            product.ProductColorID = p.ProductColorID;
            product.ProductSizeID = p.ProductSizeID;
            product.Description = p.Description;
            product.ProductInformation = p.ProductInformation;
            product.Status = Core.Enums.Status.Active;
            product.SellerID = 1;
            product.CargoID = 1;
            _productService.Add(product);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("Index", "Test");
        }
        public JsonResult LoadSubCats(int Id)
        {
            List<SubCategory> sublist = new List<SubCategory>();
            var subcatss = _tarzolDbContext.CategoryAndSubCategories.Where(x => x.CategoryID == Id).Select(i => i.SubCategoryID).ToList();
            foreach (var item in subcatss)
            {
                var s = _tarzolDbContext.SubCategories.Where(i => i.ID == item).FirstOrDefault();
                sublist.Add(s);
            }
            return Json(new SelectList(sublist, "ID", "SubCategoryName"));
        }

        public IActionResult ProductList()
        {
            var list = _tarzolDbContext.Products.Include(x=>x.SubCategory).Include(z=>z.ProductColor).Include(c=>c.ProductSize).Where(i => i.SellerID == 1).ToList();          
            return View(list);
        }

        [HttpGet]
       public IActionResult ProductImageAdd(int id)
        {
            //var result = _tarzolDbContext.Products.FirstOrDefault(i => i.ID == id);
            
            return View(id);
        }
        [HttpPost]
        public IActionResult ProductImageAdd(int pid,IFormFile fileUpload)
        {
            //ImageAdd imageadd = new ImageAdd();
            //ProductImage productimage = new ProductImage();
            //if (fileUpload != null)
            //{
            //    var extension = Path.GetExtension(fileUpload.FileName);
            //    var newimagename = Guid.NewGuid() + extension;
            //    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/Big/", newimagename);
            //    var stream = new FileStream(location, FileMode.Create);
            //    imageadd.BigImage.CopyTo(stream);
            //    productimage.BigImage = newimagename;
            //}

            //_tarzolDbContext.Set<ProductImage>().Add(productimage);
            //_tarzolDbContext.SaveChanges();
            return View();


            //if (fileUpload != null)
            //{

            //}
            //return View();
        }
    }
}
