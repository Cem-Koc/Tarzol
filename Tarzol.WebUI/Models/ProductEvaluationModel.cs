using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class ProductEvaluationModel
    {
        public string CommentContext { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int ProductRatingPoint { get; set; }
        public string returnUrl { get; set; }
        public OrderDetail OrderDetail{ get; set; }
    }
}
