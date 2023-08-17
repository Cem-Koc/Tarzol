using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Todo : BaseEntity
    {
        public int AppUserID { get; set; }
        public string TodoContext { get; set; }
        public bool isDone { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
