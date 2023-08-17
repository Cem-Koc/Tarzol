using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.Core.Enums;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var categoryList = _categoryService.GetListAll();
            return View(categoryList);
        }

        public IActionResult CategoryActiveAndPassive(int categoryId)
        {
            var category = _categoryService.GetBy(categoryId);
            if (category.Status == Core.Enums.Status.Active)
            {
                category.Status = Core.Enums.Status.Passive;
            }
            else
            {
                category.Status = Core.Enums.Status.Active;
            }

            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CategoryUpdate(int categoryId)
        {
            var category = _categoryService.GetBy(categoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(int ID, string CategoryName, string Description)
        {
            var category = _categoryService.GetBy(ID);
            category.CategoryName = CategoryName.ToUpper();
            category.Description = Description;
            _categoryService.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(CategoryAddModel categoryAddModel)
        {
            Category category = new Category();
            if (categoryAddModel.ImageUrl != null)
            {
                var extension = Path.GetExtension(categoryAddModel.ImageUrl.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/CategoryImage/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                categoryAddModel.ImageUrl.CopyTo(stream);
                category.ImageUrl = newimagename;
            }
            category.Status = categoryAddModel.Status;
            category.CategoryName = categoryAddModel.CategoryName.ToUpper();
            category.Description = categoryAddModel.Description;
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }



        public IActionResult SubCategoryIndex()
        {
            var subCategoryList = _tarzolDbContext.SubCategories.ToList();
            return View(subCategoryList);
        }


        public IActionResult SubCategoryActiveAndPassive(int subCategoryId)
        {
            var subCategory = _tarzolDbContext.SubCategories.Where(i => i.ID == subCategoryId).FirstOrDefault();
            if (subCategory.Status == Core.Enums.Status.Active)
            {
                subCategory.Status = Core.Enums.Status.Passive;
            }
            else
            {
                subCategory.Status = Core.Enums.Status.Active;
            }

            _tarzolDbContext.SubCategories.Update(subCategory);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("SubCategoryIndex");
        }

        [HttpGet]
        public IActionResult SubCategoryUpdate(int subCategoryId)
        {
            var subCategory = _tarzolDbContext.SubCategories.Where(i => i.ID == subCategoryId).FirstOrDefault();
            return View(subCategory);
        }
        [HttpPost]
        public IActionResult SubCategoryUpdate(int ID, string SubCategoryName)
        {
            var subCategory = _tarzolDbContext.SubCategories.Where(i => i.ID == ID).FirstOrDefault();
            subCategory.SubCategoryName = SubCategoryName.ToUpper();

            _tarzolDbContext.SubCategories.Update(subCategory);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("SubCategoryIndex");
        }

        [HttpGet]
        public IActionResult SubCategoryAdd()
        {

            return View();
        }
        [HttpPost]
        public IActionResult SubCategoryAdd(Status Status, string SubCategoryName)
        {
            SubCategory subCategory = new SubCategory();

            subCategory.Status = Status;
            subCategory.SubCategoryName = SubCategoryName.ToUpper();

            _tarzolDbContext.SubCategories.Update(subCategory);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("SubCategoryIndex");
        }

        public IActionResult CategoryRelations()
        {
            var categoryAndSubCategories = _tarzolDbContext.CategoryAndSubCategories.ToList();
            var categories = _tarzolDbContext.Categories.ToList();
            CategoryAndSubCategoryModel categoryAndSubCategoryModel = new CategoryAndSubCategoryModel();
            categoryAndSubCategoryModel.Categories = categories;
            categoryAndSubCategoryModel.CategoryAndSubCategories = categoryAndSubCategories;
            return View(categoryAndSubCategoryModel);
        }

        [HttpGet]
        public IActionResult CategoryRelationsDelete(int categoryId)
        {
            var category = _categoryService.GetBy(categoryId);
            var subCategory = _tarzolDbContext.CategoryAndSubCategories.Where(i => i.CategoryID == categoryId)
                .Select(x=>x.SubCategory).ToList();
            ViewBag.SubCategory = new SelectList(subCategory, "ID", "SubCategoryName");

            CategoryRelationsModel categoryRelationsModel = new CategoryRelationsModel();
            categoryRelationsModel.Category = category;
            return View(categoryRelationsModel);
        }
        [HttpPost]
        public IActionResult CategoryRelationsDelete(CategoryRelationsModel categoryRelationsModel)
        {
            var categoryAndSubCategories = _tarzolDbContext.CategoryAndSubCategories.Where(i => i.CategoryID == categoryRelationsModel.CategoryID)
                .Where(x => x.SubCategoryID == categoryRelationsModel.SubCategoryID).FirstOrDefault();
            _tarzolDbContext.CategoryAndSubCategories.Remove(categoryAndSubCategories);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("CategoryRelations");
        }

        [HttpGet]
        public IActionResult CategoryRelationsAdd(int categoryId)
        {
            var category = _categoryService.GetBy(categoryId);
            
            var subCategory = _tarzolDbContext.CategoryAndSubCategories.Where(i => i.CategoryID == categoryId)
                .Select(x => x.SubCategory).ToList();
            var subCategoryList = _tarzolDbContext.SubCategories.ToList().Except(subCategory).ToList();
           ViewBag.SubCategory = new SelectList(subCategoryList, "ID", "SubCategoryName");
            CategoryRelationsModel categoryRelationsModel = new CategoryRelationsModel();
            categoryRelationsModel.Category = category;
            return View(categoryRelationsModel);
        }

        [HttpPost]
        public IActionResult CategoryRelationsAdd(CategoryRelationsModel categoryRelationsModel)
        {
            var subCategory = _tarzolDbContext.SubCategories.Where(x => x.ID == categoryRelationsModel.SubCategoryID).FirstOrDefault();
            var category = _tarzolDbContext.Categories.Where(i => i.ID == categoryRelationsModel.CategoryID).FirstOrDefault();
            CategoryAndSubCategory categoryAndSubCategory = new CategoryAndSubCategory();
            categoryAndSubCategory.CategoryID = category.ID;
            categoryAndSubCategory.SubCategoryID = subCategory.ID;
            _tarzolDbContext.CategoryAndSubCategories.Add(categoryAndSubCategory);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("CategoryRelations");
        }
    }
}
