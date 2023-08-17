using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class MonthlySales:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public MonthlySales(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }
        public IViewComponentResult Invoke()
        {
            var result = _tarzolDbContext.OrderDetails.Select(i => i.CreatedDate).ToList();
            var orderDetail = _tarzolDbContext.OrderDetails.ToList();
            var mount = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            List<DateTime> dateTimes = new List<DateTime>();
            List<Tarzol.Entity.OrderDetail> orderDetails = new List<Entity.OrderDetail>();
            foreach (var item in result)
            {
                var convertDate = Convert.ToDateTime(item);
                var convertDateMonth = convertDate.Month;
                var convertDateYear = convertDate.Year;
                if (convertDateMonth==mount && convertDateYear==year)
                {
                    
                        dateTimes.Add(convertDate);
                   
                }
            }

            foreach (var item in dateTimes)
            {
                foreach (var order in orderDetail)
                {
                    if (order.CreatedDate==item)
                    {
                        orderDetails.Add(order);
                    }
                }
            }

            decimal total = 0;
            int counter = 0;
            foreach (var item in orderDetails)
            {
                var value = item.Quantity * item.UnitPrice;
                total += value;
                counter+=item.Quantity;
            }

            MonthlySalesModel monthlySalesModel = new MonthlySalesModel()
            {
                OrderTotalCount = counter,
                OrderTotalPrice = total
            };

            return View(monthlySalesModel);
        }
    }
}
