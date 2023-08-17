using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Business.Abstract;
using Tarzol.DataAccess.Context;
using Tarzol.Entity;
using Tarzol.WebUI.Areas.Admin.Models;

namespace Tarzol.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class MessageController : Controller
    {
        TarzolDbContext _tarzolDbContext;
        IMessageService _messageService;

        public MessageController(TarzolDbContext tarzolDbContext, IMessageService messageService)
        {
            _tarzolDbContext = tarzolDbContext;
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var mailboxReceiver = _tarzolDbContext.Messages.Where(x => x.ReceiverID == user.Id).Where(i=>i.ReceiverEmailStatus==Core.Enums.EmailStatus.Inbox).OrderByDescending(y=>y.CreatedDate).ToList();
            var mailboxSender = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i=>i.EmailStatus==Core.Enums.EmailStatus.Inbox).OrderByDescending(y=>y.CreatedDate).ToList();
            mailboxReceiver.AddRange(mailboxSender);
            var newMailbox = mailboxReceiver.OrderByDescending(i => i.CreatedDate).ToList();
            return View(newMailbox);
        }

        public IActionResult MessageDetail(int messageId)
        {
            var message = _messageService.GetBy(messageId);
            var user = _tarzolDbContext.Users.Where(i => i.Id == message.SenderID).FirstOrDefault();
            var messages = _tarzolDbContext.Messages.Where(i => i.MessageTrackingID == message.MessageTrackingID).OrderByDescending(x=>x.CreatedDate).ToList();
            message.Read = true;
            _messageService.Update(message);
            MailDetailModel mailDetailModel = new MailDetailModel()
            {
                Message = message,
                AppUser = user,
                Messages = messages
            };
            return View(mailDetailModel);
        }
        [HttpGet]
        public IActionResult ComposeMail(int sellerId)
        {
            if (sellerId!=0)
            {
                var seller = _tarzolDbContext.Sellers.Where(i => i.ID == sellerId).FirstOrDefault();
                var user = _tarzolDbContext.Users.Where(i => i.Id == seller.UserID).FirstOrDefault();
                var mailList = new SelectList(_tarzolDbContext.Users, "Id", "Email");
                foreach (var item in mailList)
                {
                    if (item.Value==user.Id.ToString())
                    {
                        item.Selected=true;
                    }
                }
                ViewBag.mailList = mailList;
            }
            else
            {
                ViewBag.mailList = new SelectList(_tarzolDbContext.Users, "Id", "Email");
            }
           
            

            return View();
        }
        [HttpPost]
        public IActionResult ComposeMail(Message message)
        {
            var senderUser = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var receiverUser = _tarzolDbContext.Users.Where(i => i.Id == message.ReceiverID).FirstOrDefault();
            var messageTrackingId = _tarzolDbContext.Messages.Select(i => i.MessageTrackingID).Max();

            message.CreatedBy = senderUser.FirstName +" "+ senderUser.LastName;
            message.Status = Core.Enums.Status.Active;
            message.SenderID = senderUser.Id;
            message.ReceiverMail = receiverUser.Email;
            message.Read = false;
            message.MessageTrackingID = messageTrackingId + 1;
            message.EmailStatus = Core.Enums.EmailStatus.SendMail;
            message.ReceiverEmailStatus = Core.Enums.EmailStatus.Inbox;
            message.SenderMail = senderUser.Email;
            _messageService.Add(message);
            return RedirectToAction("Index");
        }

        public IActionResult SendMail()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var sendMailList = _tarzolDbContext.Messages.Where(x => x.SenderID == user.Id).Where(i=>i.EmailStatus==Core.Enums.EmailStatus.SendMail).OrderByDescending(y => y.CreatedDate).ToList();
            return View(sendMailList);
        }

        [HttpGet]
        public IActionResult ReplyMail(int messageId)
        {
            var message = _messageService.GetBy(messageId);
            TempData["MessageTrackingID"] = message.MessageTrackingID;
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            if (message.SenderMail==user.Email)
            {
                ViewBag.Receiver = message.ReceiverMail;
                ViewBag.MessageTitle = message.MessageTitle;
            }
            else
            {
                ViewBag.Receiver = message.SenderMail;
                ViewBag.MessageTitle = message.MessageTitle;

            }
            var messageList = _tarzolDbContext.Messages.Where(i => i.MessageTrackingID == message.MessageTrackingID).OrderByDescending(x => x.CreatedDate).ToList();
            MailDetailModel mailDetailModel = new MailDetailModel()
            {
                Messages = messageList
            };
            return View(mailDetailModel);
        }
        [HttpPost]
        public IActionResult ReplyMail(Message message)
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var receiverUser = _tarzolDbContext.Users.Where(i => i.UserName == message.ReceiverMail).FirstOrDefault();
            message.CreatedBy = user.FirstName + " " + user.LastName;
            message.Status = Core.Enums.Status.Active;
            message.SenderID = user.Id;
            message.ReceiverID = receiverUser.Id;
            message.Read = false;
            message.MessageTrackingID = Convert.ToInt32(TempData["MessageTrackingID"]);
            message.EmailStatus = Core.Enums.EmailStatus.SendMail;
            message.ReceiverEmailStatus = Core.Enums.EmailStatus.Inbox;
            message.SenderMail = user.Email;
            _messageService.Add(message);
            return RedirectToAction("Index");
        }

        public IActionResult ImportantMail(IFormCollection formCollection)
        {
            foreach (string key in formCollection.Keys)
            {
                var keyValue = key;
           
            

            string[] messageIds=new string[keyValue.Length];
            int resultCount=0;
            string result="";
                int addResultCount = 0;
            
            for (int i = 0; i < keyValue.Length; i++)
            {
                
                
                string value = keyValue.Substring(i, 1);
                if (value!=",")
                {
                    if (resultCount==0)
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
                    messageIds[addResultCount] =result;
                    resultCount = 0;
                    result = "";
                        addResultCount++;
                }
            }
                foreach (var id in messageIds)
                {
                    if (id!=null)
                    {
                        var message = _messageService.GetBy(Convert.ToInt32(id));
                        var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
                        if (message.SenderID==user.Id)
                        {
                            message.EmailStatus = Core.Enums.EmailStatus.Important;
                        }
                        else
                        {
                            message.ReceiverEmailStatus = Core.Enums.EmailStatus.Important;
                        }
                        _messageService.Update(message);
                    }
                    
                }
            }

            return RedirectToAction("Index", "Message");
        }

        public IActionResult DeleteMail(IFormCollection formCollection)
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
                        var message = _messageService.GetBy(Convert.ToInt32(id));
                        var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
                        if (message.SenderID==user.Id)
                        {
                            message.EmailStatus = Core.Enums.EmailStatus.DeletedMail;
                        }
                        else
                        {
                            message.ReceiverEmailStatus = Core.Enums.EmailStatus.DeletedMail;
                        }
                       
                        _messageService.Update(message);
                    }

                }
            }
            return RedirectToAction("Index", "Message");
        }
        public IActionResult ReadMail(IFormCollection formCollection)
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
                        var message = _messageService.GetBy(Convert.ToInt32(id));
                        if (message.Read==false)
                        {
                            message.Read = true;
                        }
                        else
                        {
                            message.Read = false;
                        }
                       
                        _messageService.Update(message);
                    }

                }
            }


            return RedirectToAction("ImportantMailList");
        }


        public IActionResult Inbox(IFormCollection formCollection)
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
                        var message = _messageService.GetBy(Convert.ToInt32(id));
                        var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
                        if (message.SenderID==user.Id)
                        {
                            message.EmailStatus = Core.Enums.EmailStatus.Inbox;
                        }
                        else
                        {
                            message.ReceiverEmailStatus = Core.Enums.EmailStatus.Inbox;
                        }
                        if (message.SenderID==user.Id && _tarzolDbContext.Messages.Where(i=>i.MessageTrackingID==message.MessageTrackingID).Count() == 1)
                        {
                            message.EmailStatus = Core.Enums.EmailStatus.SendMail;
                        }
                        _messageService.Update(message);
                    }
                }
            }
            return RedirectToAction("ImportantMailList");
        }

        public IActionResult Refresh()
        {
            return RedirectToAction("Index","Message");
        }

        
        public IActionResult ImportantMailList()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var importantMailList = _tarzolDbContext.Messages.Where(i => i.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.Important).ToList();
            var importantMailList2 = _tarzolDbContext.Messages.Where(i => i.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.Important).ToList();
            importantMailList.AddRange(importantMailList2);
            importantMailList.OrderByDescending(i=>i.CreatedDate);
            var newImportantMailList = importantMailList.OrderByDescending(i => i.CreatedDate).ToList();
            return View(newImportantMailList);
        }

        public IActionResult DeleteMailList()
        {
            var user = _tarzolDbContext.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            var deleteMailList = _tarzolDbContext.Messages.Where(i => i.SenderID == user.Id).Where(i => i.EmailStatus == Core.Enums.EmailStatus.DeletedMail).ToList();
            var deleteMailList2 = _tarzolDbContext.Messages.Where(i => i.ReceiverID == user.Id).Where(i => i.ReceiverEmailStatus == Core.Enums.EmailStatus.DeletedMail).ToList();
            deleteMailList.AddRange(deleteMailList2);
            var newDeleteMailList = deleteMailList.OrderByDescending(i => i.CreatedDate).ToList();
            return View(newDeleteMailList);
        }


    }
}
