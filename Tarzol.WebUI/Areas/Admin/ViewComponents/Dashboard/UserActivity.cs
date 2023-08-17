using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class UserActivity:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public UserActivity(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var orders = _tarzolDbContext.Orders.ToList();
            List<Tarzol.Entity.Order> newOrders = new List<Entity.Order>();
            var favorites = _tarzolDbContext.Favorites.ToList();
            List<Tarzol.Entity.Favorite> newFavorites = new List<Entity.Favorite>();

            foreach (var order in orders)
            {
                if (Convert.ToDateTime(order.CreatedDate).ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    newOrders.Add(order);
                }
            }
            foreach (var favorite in favorites)
            {
                if (Convert.ToDateTime(favorite.CreatedDate).ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    newFavorites.Add(favorite);
                }
            }

            UserActivityModel userActivityModel = new UserActivityModel()
            {
                ActivityCount = newOrders.Count() + newFavorites.Count(),
                UserCount=newOrders.Select(i=>i.CreatedBy).Distinct().Count() + newFavorites.Select(i=>i.AppUserID).Distinct().Count()
            };
            return View(userActivityModel);
        }
    }
}
