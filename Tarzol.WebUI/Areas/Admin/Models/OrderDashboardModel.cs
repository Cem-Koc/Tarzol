using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Areas.Admin.Models
{
    public class OrderDashboardModel
    {
        public int TotalOrder { get; set; }
        public int LastMonthOrders { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string[] Data2 { get; set; }
        public string Data3 { get; set; }
    }
}
