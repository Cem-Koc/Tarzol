using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IFavoriteService _favoriteService;

        public FavoriteController(TarzolDbContext tarzolDbContext, IFavoriteService favoriteService)
        {
            _tarzolDbContext = tarzolDbContext;
            _favoriteService = favoriteService;
        }

        public IActionResult Index()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var userFavorites= _favoriteService.GetListAll(i => i.AppUserID == user.Id).ToList();

            FavoriteProductsModel favoriteProductsModel = new FavoriteProductsModel()
            {
                Favorites = userFavorites,
                Products = _tarzolDbContext.Products.Include("ProductSize").Include("ProductColor").ToList()
            }; 
            return View(favoriteProductsModel);
        }

        public IActionResult DeleteFavorite(int favoriteId, string returnUrl)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            var favoriteproduct = _favoriteService.GetBy(favoriteId);
            _favoriteService.Delete(favoriteproduct);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Favorite");
        }

        public IActionResult AddFavorite(int productId, string returnUrl)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            
            if (user==null)
            {
                TempData["Id"] = productId;
                return RedirectToAction("Index", "Login",new { returnUrl=returnUrl });
            }
            else if (_favoriteService.GetListAll(x=>x.AppUserID==user.Id).Count()>0 && _favoriteService.GetListAll(i=>i.ProductID==productId).Count()>0)
            {
                var favorites = _favoriteService.GetListAll(i => i.ProductID == productId).ToList();
                var favoriteProduct = favorites.Where(i => i.AppUserID == user.Id).FirstOrDefault();
                return RedirectToAction("DeleteFavorite", "Favorite",new { favoriteId=favoriteProduct.ID , returnUrl = returnUrl });
            }
            else
            {
                var productID = Convert.ToInt32(TempData["Id"]);
                if (productID!=0)
                {
                    productId = productID;
                }
                Favorite favorite = new Favorite()
                {
                    AppUserID = user.Id,
                    CreatedDate = DateTime.Now,
                    CreatedBy = user.UserName,
                    ProductID = productId,
                    Status = Core.Enums.Status.Active
                };
                var result = _favoriteService.GetListAll(i => i.AppUserID == favorite.AppUserID).ToList();
                if (result.Where(i=>i.ProductID==favorite.ProductID).Count()==0)
                {
                    _favoriteService.Add(favorite);
                }
                
                return RedirectToAction("ViewProduct","Product",new {id= productId });
            }

            
        }
    }
}
