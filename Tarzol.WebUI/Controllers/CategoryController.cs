using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        ICategoryService _categoryService;

        public CategoryController(TarzolDbContext tarzolDbContext, ICategoryService categoryService)
        {
            _tarzolDbContext = tarzolDbContext;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProductByCategoryList(int ID,int SubCategoryID)
        {
            ViewBag.categoryname = _tarzolDbContext.Categories.Where(i => i.ID == ID).Select(x=>x.CategoryName).FirstOrDefault();
            ViewBag.subcategoryname = _tarzolDbContext.SubCategories.Where(i => i.ID == SubCategoryID).Select(x=>x.SubCategoryName).FirstOrDefault();
            var results = _tarzolDbContext.Products.Include("Category").Include("SubCategory").Where(i => i.CategoryID == ID).ToList().Where(x => x.SubCategoryID == SubCategoryID).ToList().Where(v=>v.Status==Core.Enums.Status.Active).ToList();
            return View(results);
        }

       
    }
}
