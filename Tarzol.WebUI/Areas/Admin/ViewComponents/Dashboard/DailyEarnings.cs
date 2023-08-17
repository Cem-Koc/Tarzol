using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class DailyEarnings:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public DailyEarnings(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var todayEarnings = _tarzolDbContext.OrderDetails.ToList();
            
            decimal totalEarnings = 0;
            List<Tarzol.Entity.OrderDetail> newTodayEarnings = new List<Entity.OrderDetail>();
            foreach (var item in todayEarnings)
            {
                if (Convert.ToDateTime(item.CreatedDate).ToShortDateString()==DateTime.Now.ToShortDateString())
                {
                    newTodayEarnings.Add(item);
                }
            }
            foreach (var orderDetail in newTodayEarnings)
            {
                var earnings = orderDetail.Quantity * orderDetail.UnitPrice;
                totalEarnings += earnings;
            }

            var todayEarningsOrderCount = newTodayEarnings.Select(i=>i.OrderID).Distinct().Count();

            DailyEarningsModel dailyEarningsModel = new DailyEarningsModel()
            {
                TodayEarningsOrderCount = todayEarningsOrderCount,
                TotalEarnings = totalEarnings
            };
            return View(dailyEarningsModel);
        }
    }
}
