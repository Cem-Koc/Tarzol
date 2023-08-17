using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.ViewComponents.Seller
{
    public class NewSeller2 : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public NewSeller2(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var sellers = _tarzolDbContext.Sellers.OrderByDescending(i => i.ID).Take(2).ToList();
            return View(sellers);
        }
    }
}
