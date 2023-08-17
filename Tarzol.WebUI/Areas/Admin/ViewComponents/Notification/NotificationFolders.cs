using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Notification
{
    public class NotificationFolders : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public NotificationFolders(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }
        public IViewComponentResult Invoke()
        {

            NotificationFoldersModel notificationFoldersModel = new NotificationFoldersModel()
            {
                NewNotificationCount = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Count(),
                ReadNotificationCount = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Passive).Count()
            };
            return View(notificationFoldersModel);
        }
    }
}
