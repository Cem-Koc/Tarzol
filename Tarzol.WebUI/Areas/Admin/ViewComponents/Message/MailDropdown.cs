using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Message
{
    public class MailDropdown : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;
        public MailDropdown(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var mailList = _tarzolDbContext.Messages.Where(i => i.ReceiverID == user.Id).Where(i => i.Read == false).ToList();
            return View(mailList);
        }
    }
}
