using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    BirthDate = p.BirthDate,
                    UserName=p.Email,
                    Status=Core.Enums.Status.Active,
                    CreatedDate=DateTime.Now
                };
                if (p.Password==p.ComfirmPassword)
                {
                    var result = await _userManager.CreateAsync(user, p.Password);
                    var result2 = await _userManager.AddToRoleAsync(user, "User");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                
            }
            return View(p);
        }
    }
}
