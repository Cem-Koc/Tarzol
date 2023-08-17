using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class YearlyOrder:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public YearlyOrder(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var year = DateTime.Now.Year;
            var orderList = _tarzolDbContext.Orders.ToList();
            List<Tarzol.Entity.Order> orders = new List<Tarzol.Entity.Order>();
            var orderDetailList = _tarzolDbContext.OrderDetails.ToList();
            int productCount = 0;
            foreach (var order in orderList)
            {
                if (Convert.ToDateTime(order.CreatedDate).Year==year)
                {
                    orders.Add(order);
                }
            }

            foreach (var order in orders)
            {
                foreach (var orderDetail in orderDetailList)
                {
                    if (order.ID==orderDetail.OrderID)
                    {
                        productCount += orderDetail.Quantity;
                    }
                }
            }
            YearlyOrderModel yearlyOrderModel = new YearlyOrderModel()
            {
                ProductCount = productCount,
                YearlyOrderCount = orders.Count
            };

            return View(yearlyOrderModel);
        }
    }
}
