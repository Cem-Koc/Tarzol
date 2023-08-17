using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class ProductInQuestionModel
    {
        public List<Product> Products{ get; set; }
        public List<Question> Questions{ get; set; }
        public List<Answer> Answers{ get; set; }
    }
}
