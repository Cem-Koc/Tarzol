using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class MailFoldersModel
    {
        public int InboxCount { get; set; }
        public int InboxUnreadCount { get; set; }
        public int SendMailCount { get; set; }
       
        public int ImportantCount { get; set; }
        public int ImportantUnreadCount { get; set; }
        public int DeletedMailCount { get; set; }
        public int DeletedMailUnreadCount { get; set; }
    }
}
