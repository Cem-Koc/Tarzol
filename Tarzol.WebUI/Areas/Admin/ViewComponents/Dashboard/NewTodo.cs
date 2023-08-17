using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class NewTodo:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public NewTodo(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
