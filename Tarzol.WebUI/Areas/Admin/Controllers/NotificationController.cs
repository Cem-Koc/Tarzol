using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        INotificationService _notificationService;

        public NotificationController(TarzolDbContext tarzolDbContext, INotificationService notificationService)
        {
            _tarzolDbContext = tarzolDbContext;
            _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            var newNotification = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).OrderByDescending(i=>i.CreatedDate).ToList();
            return View(newNotification);
        }

        public IActionResult ReadNotification()
        {
            var newNotification = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Passive).OrderByDescending(i => i.CreatedDate).ToList();
            return View(newNotification);
        }
        public IActionResult MarkNotificationAsRead(IFormCollection formCollection)
        {
            foreach (string key in formCollection.Keys)
            {
                var keyValue = key;
                string[] notificationIds = new string[keyValue.Length];
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
                        notificationIds[addResultCount] = result;
                        resultCount = 0;
                        result = "";
                        addResultCount++;
                    }
                }
                foreach (var id in notificationIds)
                {
                    if (id != null)
                    {
                        var notification = _notificationService.GetBy(Convert.ToInt32(id));
                        notification.Status = Core.Enums.Status.Passive;

                        _notificationService.Update(notification);
                    }
                }
            }
            return RedirectToAction("Index");
        }



    }
}
