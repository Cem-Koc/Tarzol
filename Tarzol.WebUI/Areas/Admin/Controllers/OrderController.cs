using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IOrderService _orderService;

        public OrderController(TarzolDbContext tarzolDbContext, IOrderService orderService)
        {
            _tarzolDbContext = tarzolDbContext;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllOrders()
        {
            var orderList=_orderService.GetListAll();
            return View(orderList);
        }
        public IActionResult OrderDetail(int orderId)
        {
            var orderDetail = _tarzolDbContext.OrderDetails.Where(i => i.OrderID == orderId).ToList();
            ViewBag.orderTotal = _tarzolDbContext.OrderDetails.Where(i => i.OrderID == orderId).Sum(x => x.UnitPrice);
            return View(orderDetail);
        }

        public IActionResult OrderActive()
        {
            var orderList = _orderService.GetListAll(i=>i.DelivreyDate==null);
            return View(orderList);
        }
        public IActionResult OrderPassive()
        {
            var orderList = _orderService.GetListAll(i => i.DelivreyDate != null);
            return View(orderList);
        }
    }
}
