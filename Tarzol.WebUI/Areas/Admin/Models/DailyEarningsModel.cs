using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class DailyEarningsModel
    {
        public int TodayEarningsOrderCount { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
