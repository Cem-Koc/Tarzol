using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.Business.Concrete;
using Tarzol.DataAccess.Concrete.EfRepository;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Models;

namespace Tarzol.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TarzolDbContext _tarzolDbContext;
        ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, TarzolDbContext tarzolDbContext,ICategoryService categoryService)
        { 
            _logger = logger;
            _tarzolDbContext = tarzolDbContext;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
