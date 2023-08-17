using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Concrete;
using Tarzol.DataAccess.Concrete.EfRepository;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;

namespace Tarzol.WebUI.ViewComponents.Category
{
    public class AllCategoryList:ViewComponent
    {
        
        TarzolDbContext _tarzolDbContext;

        public AllCategoryList(TarzolDbContext tarzolDbContext)
        {
           
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            //var values = _categoryManager.GetListAll();
            //var catid = _tarzolDbContext.Categories.Select(i => i.ID).ToList();
            //List<CategoryAndSubCategory> categoryAndSubCategory = new List<CategoryAndSubCategory>();

            //foreach (var item in catid)
            //{
            //    var c = _tarzolDbContext.CategoryAndSubCategories.Where(i => i.CategoryID == item).FirstOrDefault();
            //    categoryAndSubCategory.Add(c);
            //}
            var values = _tarzolDbContext.CategoryAndSubCategories.Include("Category").Include("SubCategory").ToList();
          
            return View(values);
        }
    }
}
