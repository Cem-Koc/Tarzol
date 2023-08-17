using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Abstract;
using Tarzol.Core.Enums;

namespace Tarzol.Entity
{
   public class AppRole:IdentityRole<int>,IEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Status Status { get; set; }
    }
}
