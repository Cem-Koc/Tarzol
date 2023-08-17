using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        TarzolDbContext _tarzolDbContext;
        public LoginController(SignInManager<AppUser> signInManager,RoleManager<AppRole> roleManager, TarzolDbContext tarzolDbContext)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tarzolDbContext = tarzolDbContext;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.url = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel p,string returnUrl)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == p.UserName).FirstOrDefault();
            var userRole = _tarzolDbContext.UserRoles.Where(i => i.UserId == user.Id).Select(x => x.RoleId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, false, true);
                var role = await _roleManager.FindByIdAsync(Convert.ToString(userRole));
                if (result.Succeeded)
                {
                    if (role.Name== "Administrator")
                    {
                        return Redirect("/Admin/Dashboard/DashboardIndex");
                    }
                    else if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
