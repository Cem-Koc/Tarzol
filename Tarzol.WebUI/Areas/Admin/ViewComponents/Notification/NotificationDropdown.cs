using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Notification
{
    public class NotificationDropdown : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;
        public NotificationDropdown(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var notificationAllCount = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Count();
            var negativeNoticeCount=_tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NegativeNotice).Count();
            var negativeNoticeLastDate = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NegativeNotice).Max(i => i.CreatedDate).ToString();
            var NewProductCount = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NewProduct).Count();
            var NewProductLastDate = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NewProduct).Max(i => i.CreatedDate).ToString();
            var NewSellerCount = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NewSeller).Count();
            var NewSellerLastDate = _tarzolDbContext.Notifications.Where(i => i.Status == Core.Enums.Status.Active).Where(i => i.NotificationStatus == Core.Enums.NotificationStatus.NewSeller).Max(i => i.CreatedDate).ToString();
            NotificationDropdownModel notificationDropdownModel = new NotificationDropdownModel()
            {
                
                NotificationAllCount = Convert.ToString(notificationAllCount),
                NegativeNoticeCount = Convert.ToString(negativeNoticeCount),
                NegativeNoticeLastDate = negativeNoticeLastDate,
                NewProductCount = Convert.ToString(NewProductCount),
                NewProductLastDate = NewProductLastDate,
                NewSellerCount = Convert.ToString(NewSellerCount),
                NewSellerLastDate = NewSellerLastDate,
            };


            return View(notificationDropdownModel);
        }
    }
}
