 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DTO
{
    public class SaleDetailDTO
    {
        public int? Amount { get; set; }

        public string? Price { get; set; }

        public string? Total { get; set; }

        public int? ProductId { get; set; }

        public string? ProductDescription { get; set; }
    }
}
