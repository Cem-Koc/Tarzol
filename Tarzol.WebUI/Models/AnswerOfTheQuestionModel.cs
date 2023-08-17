using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class AnswerOfTheQuestionModel
    {
        public Answer Answer{ get; set; }
        public List<Question> Questions{ get; set; }
        public Question Question { get; set; }
    }
}
