using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.ViewComponents.Seller
{
    public class SellerDashboard : ViewComponent
    {
        public IViewComponentResult Invoke()
        {            
            return View();
        }
    }
}
