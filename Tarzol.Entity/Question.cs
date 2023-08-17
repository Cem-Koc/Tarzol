using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Question : BaseEntity
    {
        public string QuestionContent { get; set; }
        public int ProductID { get; set; }
        public int AppUserID { get; set; }


        public virtual Product Product { get; set; }
        
        public virtual AppUser AppUser { get; set; }
    }
}
