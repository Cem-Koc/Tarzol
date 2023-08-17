
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.DataAccess.Context;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.ViewComponents.Message
{
    public class MailFolders : ViewComponent
    {
        TarzolDbContext _tarzolDbContext;

        public MailFolders(TarzolDbContext tarzolDbContext)
        {
            _tarzolDbContext = tarzolDbContext;
        }

        public IViewComponentResult Invoke()
        {

            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var receiverInboxCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.Inbox).Count();
            var senderInboxCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.Inbox).Count();
            var unreadReceiverInboxCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.Inbox).Where(x => x.Read == false).Count();
            var unreadSenderInboxCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.Inbox).Where(x => x.Read == false).Count();

            var receiverImportantCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.Important).Count();
            var senderImportantCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.Important).Count();
            var unreadReceiverImportantCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.Important).Where(x => x.Read == false).Count();
            var unreadSenderImportantCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.Important).Where(x => x.Read == false).Count();
            var receiverDeleteCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.DeletedMail).Count();
            var senderDeleteCount= _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.DeletedMail).Count();
            var unreadReceiverDeleteCount = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.DeletedMail).Where(x => x.Read == false).Count();
            var unreadSenderDeleteCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.DeletedMail).Where(x => x.Read == false).Count();

            MailFoldersModel mailFoldersModel = new MailFoldersModel()
            {
                InboxCount = receiverInboxCount + senderInboxCount,
                InboxUnreadCount = unreadReceiverInboxCount + unreadSenderInboxCount,
                SendMailCount = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.SendMail).Count(),
                ImportantCount = receiverImportantCount+ senderImportantCount,
                ImportantUnreadCount = unreadReceiverImportantCount+ unreadSenderImportantCount,
                DeletedMailCount = receiverDeleteCount + senderDeleteCount,
                DeletedMailUnreadCount = unreadReceiverDeleteCount + unreadSenderDeleteCount
            };
            return View(mailFoldersModel);
        }
    }
}
