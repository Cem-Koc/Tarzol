using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.Core.Concrete;

namespace Tarzol.Entity
{
   public class Answer : BaseEntity
    {
        public string AnswerContent { get; set; }
        public int QuestionID { get; set; }
        public int ProductID { get; set; }
        public int SellerID { get; set; }


        public virtual Product Product { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual Question Question { get; set; }
    }
}
