using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
   public class Notification : BaseEntity
    {
        public int CreativeID { get; set; }
        public int? CreativeInteractionID { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        
    }
}
