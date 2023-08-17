using Microsoft.AspNetCore.Mvc;
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
    public class OrderController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IOrderService _orderService;
        IOrderDetailService _orderDetailService;

        public OrderController(TarzolDbContext tarzolDbContext, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _tarzolDbContext = tarzolDbContext;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public IActionResult Index()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var userOrder = _orderService.GetListAll(i => i.CreatedBy == user.UserName);
            var userCustomer = _tarzolDbContext.Customers.Where(i => i.AppUserID == user.Id).ToList();
            var userOrderDetail = _orderDetailService.GetListAll(i => i.CreatedBy == user.UserName);
            var userOrderProduct = _tarzolDbContext.Products.ToList();

            UserOrderDetails userOrderDetails = new UserOrderDetails()
            {
                OrderDetails = userOrderDetail,
                Orders = userOrder,
                Products = userOrderProduct,
                Customers = userCustomer
            };
            return View(userOrderDetails);
        }

        [HttpGet]
        public IActionResult ConfirmedOrder(int customerId)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            TempData["customerId"] = customerId;
            var productList = _tarzolDbContext.Products.ToList();
            return View(productList);
        }
        [HttpPost]
        public IActionResult ConfirmedOrder()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            Order order = new Order()
            {
                CreatedDate = DateTime.Now,
                CustomerID = (int)TempData["customerId"],
                CreatedBy = user.UserName,
                OrderDate = DateTime.Now,
                Status = Core.Enums.Status.Active,
                EstimatedDelivreyDate = DateTime.Now.AddDays(10),
                OrderStatus = Core.Enums.OrderStatus.CargoReceived
            };
            var orderList= _orderService.GetListAll();
            Random random = new Random();
            int number = random.Next(10000, 99999);
            if (orderList.Count()==0)
            {
                order.OrderNumber = number;
            }
            else
            {
                bool successful = true;
                do
                {
                    
                    Random randomvalue = new Random();
                    int newOrderNumber = random.Next(10000, 99999);
                    if (orderList.Where(i=>i.OrderNumber== newOrderNumber).Count()==0)
                    {
                        order.OrderNumber = newOrderNumber;
                        successful = false;
                    }                  
                } while (successful);
               
            }
            _orderService.Add(order);

            foreach (var item in cart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    CreatedDate = DateTime.Now,
                    OrderID = order.ID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Status = Core.Enums.Status.Active,
                    UnitPrice = _tarzolDbContext.Products.Where(i => i.ID == item.ProductID).Select(x => x.DiscountedPrice).FirstOrDefault(),
                    WasDelivered = false,
                    ID = _tarzolDbContext.Products.Where(i=>i.ID==item.ProductID).Select(x=>x.SellerID).FirstOrDefault(),
                    CreatedBy=user.UserName
                };
                _orderDetailService.Add(orderDetail);
            }

            HttpContext.Session.Remove("cart");

            return RedirectToAction("Index","Home");
        }


        public IActionResult OrderDetailPage(int orderId)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var order = _orderService.GetBy(orderId);
            var customer = _tarzolDbContext.Customers.Where(i => i.ID == order.CustomerID).FirstOrDefault();
            var orderDetailList = _orderDetailService.GetListAll(i => i.OrderID == orderId);
            var userOrderProduct = _tarzolDbContext.Products.ToList();

            OrderDetailPageModel orderDetailPageModel = new OrderDetailPageModel()
            {
               Order=order,
               Customer=customer,
               orderDetails=orderDetailList,
               Products=userOrderProduct
            };
            return View(orderDetailPageModel);
        }

      
    }
}
