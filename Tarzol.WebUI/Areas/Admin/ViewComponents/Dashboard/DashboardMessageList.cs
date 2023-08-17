using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardMessageList : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public DashboardMessageList(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var unreadMessageList = _tarzolDbContext.Messages.Where(i => i.Read == false).Where(i=>i.ReceiverMail==User.Identity.Name).OrderByDescending(i => i.CreatedDate).ToList();
            ViewBag.unreadMessageListCount = unreadMessageList.Count();
            if (unreadMessageList.Count<5)
            {
                var readMessageList = _tarzolDbContext.Messages.Where(i => i.Read == true).Where(i => i.ReceiverMail == User.Identity.Name).OrderByDescending(i => i.CreatedDate).ToList();
                if (readMessageList.Count()!=0)
                {
                    for (int i = unreadMessageList.Count; i < 5; i++)
                    {
                        var message = readMessageList.Take(1).FirstOrDefault();
                        unreadMessageList.Add(message);
                    }
                }
                
            };

            return View(unreadMessageList);
        }
    }
}
