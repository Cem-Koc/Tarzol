using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class TodoList:ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public TodoList(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {

            var todolist = _tarzolDbContext.Todos.Where(i=>i.AppUser.UserName==User.Identity.Name).ToList();
            return View(todolist);
        }
    }
}
