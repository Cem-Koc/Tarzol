using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Entity;

namespace Tarzol.WebUI.Models
{
    public class SellerImageModel
    {
        public int UserID { get; set; }
        public string CompanyName { get; set; }
        public string SellerAddress { get; set; }
        public string TaxNumber { get; set; }
        public string ProfileImageUrl { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public IFormFile SellerImage { get; set; }
    }
}
