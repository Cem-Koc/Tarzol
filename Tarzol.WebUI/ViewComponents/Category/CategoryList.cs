using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.ViewComponents.Category
{
    public class CategoryList : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;
        ICategoryService _categoryService;

        public CategoryList(TarzolDbContext tarzolDbContext, ICategoryService categoryService)
        {
            _tarzolDbContext = tarzolDbContext;
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            AllCategoryModel allCategoryModel = new AllCategoryModel
            {
                Categories = _tarzolDbContext.Categories.ToList(),
                SubCategories = _tarzolDbContext.SubCategories.ToList(),
                CategoryAndSubCategories = _tarzolDbContext.CategoryAndSubCategories.ToList()

            };

            return View(allCategoryModel);
        }


    }
}
