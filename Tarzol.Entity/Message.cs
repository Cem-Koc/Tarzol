using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
   public class Message : BaseEntity
    {
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
        public bool Read { get; set; }
        public int MessageTrackingID { get; set; }

        public EmailStatus EmailStatus { get; set; }
        public EmailStatus ReceiverEmailStatus { get; set; }

        public string SenderMail{ get; set; }
        public string ReceiverMail{ get; set; }
    }
}
