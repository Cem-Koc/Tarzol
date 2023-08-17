using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.ViewComponents.Category
{
    public class Deneme : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public Deneme(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
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
