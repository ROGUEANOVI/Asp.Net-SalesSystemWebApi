using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DTO
{
    public class DashboardDTO
    {
        public int SalesTotal { get; set; }

        public string? IncomesTotal { get; set; }

        public int ProductsTotal { get; set; }

        public List<WeekSalesDTO>? LastWeekSales { get; set; }
    }
}
