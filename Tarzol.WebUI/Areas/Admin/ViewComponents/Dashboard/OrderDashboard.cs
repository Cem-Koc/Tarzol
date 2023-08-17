using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class OrderDashboard:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public OrderDashboard(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            //var mount = DateTime.Now.Month;
            //var year = DateTime.Now.Year;
            //var orders = _tarzolDbContext.Orders.ToList();
            //List<Tarzol.Entity.Order> newOrderList = new List<Entity.Order>();
            //List<Tarzol.Entity.Order> LastMonthOrderList = new List<Entity.Order>();
            //var orderDetailsList = _tarzolDbContext.OrderDetails.ToList();
            //decimal monthlyIncome = 0;

            //foreach (var order in orders)
            //{
            //    var convertDate = Convert.ToDateTime(order.CreatedDate);
            //    var convertDateMonth = convertDate.Month;
            //    var convertDateYear = convertDate.Year;
            //    if (convertDateMonth == mount && convertDateYear == year)
            //    {
            //        newOrderList.Add(order);
            //    }
            //    if (convertDateMonth == (mount-1) && convertDateYear == year)
            //    {
            //        LastMonthOrderList.Add(order);
            //    }
            //}

            //foreach (var order in newOrderList)
            //{
            //    foreach (var orderDetail in orderDetailsList)
            //    {
            //        if (order.ID==orderDetail.OrderID)
            //        {
            //            monthlyIncome += orderDetail.Quantity * orderDetail.UnitPrice;
            //        }
            //    }
            //}

            ////var strMonth = mount.ToString();
            ////var strYear = year.ToString();
            ////var strOneDay = "01." + strMonth + "." + strYear;
            ////DateTime oneDay = Convert.ToDateTime(strOneDay);

            //DateTime firstDate = new DateTime(year, mount, 01);
            //DateTime lastDate = firstDate.AddMonths(1).AddDays(-1);
            //int counterDate = 1;
            //int[] orderCount = new int[lastDate.Day];

            //for (int i = 0; i < (lastDate - firstDate).Days; i++)
            //{
            //    foreach (var order in newOrderList)
            //    {
            //        if (order.CreatedDate.Value.Day == counterDate)
            //        {
            //            var dailyOrderCount = _tarzolDbContext.Orders.Where(i => i.CreatedDate.Value.Day == counterDate).Where(i=>i.CreatedDate.Value.Month==mount).Count();
            //            orderCount.SetValue(dailyOrderCount, i);
            //        }
            //        else
            //        {
            //            var dailyOrderCount = 0;
            //            orderCount.SetValue(dailyOrderCount, i);
            //        }
            //    }
            //    counterDate++;
            //}


            //string[] data2List = new string[lastDate.Day];
            //var counter = 0;
            //foreach (var item in orderCount)
            //{
            //    counter++;
            //    string value = "[gd(" + year.ToString() + ", " + mount.ToString() + ", " + counter.ToString() + "), " + item.ToString() + "],";
            //    data2List.SetValue(value, counter - 1);
            //}


            //OrderDashboardModel orderDashboardModel = new OrderDashboardModel()
            //{
            //    TotalOrder = newOrderList.Count(),
            //    LastMonthOrders= LastMonthOrderList.Count(),
            //    MonthlyIncome= monthlyIncome,
            //    Data2=data2List,
            //};

            /////////////////////////////////////////////////////////////////
            var orderCount1 = 0;
            var productCount1 = 0;
            var orderCount2 = 0;
            var productCount2 = 0;
            var orderCount3 = 0;
            var productCount3 = 0;
            var orderCount4 = 0;
            var productCount4 = 0;
            var orderCount5 = 0;
            var productCount5 = 0;
            var orderCount6 = 0;
            var productCount6 = 0;
            var orderCount7 = 0;
            var productCount7 = 0;

            decimal totalPrice = 0;

            var yesterday = DateTime.Now.AddDays(-1).ToShortDateString();
            var orders = _tarzolDbContext.Orders.ToList();
            var orderDetails = _tarzolDbContext.OrderDetails.ToList();
            foreach (var order in orders)
            {
                var orderStringDate=Convert.ToDateTime(order.CreatedDate).ToShortDateString();
                if (orderStringDate==yesterday)
                {
                    var orderPrice = _tarzolDbContext.OrderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity * i.UnitPrice);
                    totalPrice += orderPrice;
                    var orderHour = Convert.ToDateTime(order.CreatedDate).Hour;
                    if (orderHour>=12&&orderHour<16)
                    {
                        orderCount1 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount1 += productCount;
                    }
                    else if (orderHour >= 16 && orderHour < 20)
                    {
                        orderCount2 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount2 += productCount;
                    }
                    else if (orderHour >= 20 && orderHour < 22)
                    {
                        orderCount3 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount3 += productCount;
                    }
                    else if (orderHour >= 22 && orderHour < 00)
                    {
                        orderCount4 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount4 += productCount;
                    }
                    else if (orderHour >= 00 && orderHour < 04)
                    {
                        orderCount5 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount5 += productCount;
                    }
                    else if (orderHour >= 04 && orderHour < 08)
                    {
                        orderCount6 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount6 += productCount;
                    }
                    else if (orderHour >= 08 && orderHour < 12)
                    {
                        orderCount7 += 1;
                        var productCount = orderDetails.Where(i => i.OrderID == order.ID).Sum(i => i.Quantity);
                        productCount7 += productCount;
                    }
                }
            }

            TempData["orderCount1"] = orderCount1;
            TempData["productCount1"] = productCount1;
            TempData["orderCount2"] = orderCount2;
            TempData["productCount2"] = productCount2;
            TempData["orderCount3"] = orderCount3;
            TempData["productCount3"] = productCount3;
            TempData["orderCount4"] = orderCount4;
            TempData["productCount4"] = productCount4;
            TempData["orderCount5"] = orderCount5;
            TempData["productCount5"] = productCount5;
            TempData["orderCount6"] = orderCount6;
            TempData["productCount6"] = productCount6;
            TempData["orderCount7"] = orderCount7;
            TempData["productCount7"] = productCount7;

            TempData["totalPrice"] = totalPrice.ToString("0.00");
            return View();
        }
    }
}
