using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        TarzolDbContext _tarzolDbContext;

        public DashboardController(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashboardIndex()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewTodo(Todo todo)
        {
            Todo newTodo = new Todo()
            {
                CreatedDate = todo.CreatedDate,
                isDone = false,
                Status = Core.Enums.Status.Active,
                TodoContext = todo.TodoContext,
                AppUserID = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).Select(i => i.Id).FirstOrDefault(),
                CreatedBy = User.Identity.Name
            };

            _tarzolDbContext.Todos.Add(newTodo);
            _tarzolDbContext.SaveChanges();
            return RedirectToAction("DashboardIndex");
        }

        public IActionResult TodoChange(IFormCollection formCollection)
        {
            foreach (string key in formCollection.Keys)
            {
                var keyValue = key;



                string[] messageIds = new string[keyValue.Length];
                int resultCount = 0;
                string result = "";
                int addResultCount = 0;

                for (int i = 0; i < keyValue.Length; i++)
                {


                    string value = keyValue.Substring(i, 1);
                    if (value != ",")
                    {
                        if (resultCount == 0)
                        {
                            resultCount++;
                            result = value;
                        }
                        else
                        {
                            result += value;
                        }
                    }
                    else
                    {
                        messageIds[addResultCount] = result;
                        resultCount = 0;
                        result = "";
                        addResultCount++;
                    }
                }
                foreach (var id in messageIds)
                {
                    if (id != null)
                    {

                        var todo = _tarzolDbContext.Todos.Where(i => i.ID == Convert.ToInt32(id)).FirstOrDefault();
                        if (todo.isDone==true)
                        {
                            todo.isDone = false;
                            _tarzolDbContext.Todos.Update(todo);
                            _tarzolDbContext.SaveChanges();
                        }
                        else
                        {
                            todo.isDone = true;
                            _tarzolDbContext.Todos.Update(todo);
                            _tarzolDbContext.SaveChanges();
                        }
                    }
                }
                }
                return RedirectToAction("DashboardIndex");
        }

    }
}
