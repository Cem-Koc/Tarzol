using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Models
{
    public class ProductWithImageAdd
    {
        public string ProductName { get; set; }
        public decimal MarketPrice { get; set; }
        public int UnitsInStock { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string TradeMark { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string ProductInformation { get; set; }
        public int CargoID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int ProductColorID { get; set; }
        public int ProductSizeID { get; set; }
        public int SellerID { get; set; }

        public IFormFile ImageOne { get; set; }
        public IFormFile ImageTwo { get; set; }
        public IFormFile ImageThree { get; set; }
        public IFormFile ImageFour { get; set; }
    }
}
