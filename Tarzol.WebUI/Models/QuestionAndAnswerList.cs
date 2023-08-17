using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class QuestionAndAnswerList
    {
        public List<Answer> Answers{ get; set; }
        public List<Question> Questions{ get; set; }
        public Seller Seller { get; set; }
        public Question Question { get; set; }
    }
}
