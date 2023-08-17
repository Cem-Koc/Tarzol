using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class MailDetailModel
    {
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }
        public AppUser AppUser{ get; set; }
    }
}
