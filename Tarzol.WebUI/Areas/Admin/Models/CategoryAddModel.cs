using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarzol.Core.Enums;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class CategoryAddModel
    {
        public IFormFile ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

    }
}
