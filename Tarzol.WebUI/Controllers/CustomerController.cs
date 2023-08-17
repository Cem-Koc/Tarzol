using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CustomerController : Controller
    {
        ICustomerService _customerService;
        TarzolDbContext _tarzolDbContext;
        public CustomerController(ICustomerService customerService, TarzolDbContext tarzolDbContext)
        {
            _customerService = customerService;
            _tarzolDbContext = tarzolDbContext;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            ViewBag.City = new SelectList(_tarzolDbContext.Cities, "ID", "CityName");
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();         
            customer.AppUserID = user.Id;
            customer.CreatedBy = user.UserName;
            customer.Status = Core.Enums.Status.Active;
            customer.CreatedDate = DateTime.Now;
            _customerService.Add(customer);
            return RedirectToAction("ConfirmedOrder","Order",new { customerId=customer.ID });
        }
        public JsonResult LoadDistrict(int Id)
        {
            var district = _tarzolDbContext.Districts.Where(x => x.CityID == Id).ToList();
            return Json(new SelectList(district, "ID", "DistrictName"));
        }

       

    }
}
