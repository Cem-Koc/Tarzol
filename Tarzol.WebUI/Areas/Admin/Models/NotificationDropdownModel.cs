using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class NotificationDropdownModel
    {
        public string NotificationAllCount { get; set; }
        public string NewSellerCount { get; set; }
        public string NewSellerLastDate { get; set; }
        public string NewProductCount { get; set; }
        public string NewProductLastDate { get; set; }
        public string NegativeNoticeCount { get; set; }
        public string NegativeNoticeLastDate { get; set; }
    }
}
