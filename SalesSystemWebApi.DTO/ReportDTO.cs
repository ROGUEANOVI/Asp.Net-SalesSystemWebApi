using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DTO
{
    public class ReportDTO
    {
        public string? TicketNumber { get; set; }
        
        public string? PaymentType { get; set; }

        public string? RegistrationDate { get; set; }
        
        public string? Product { get; set; }

        public int Amount { get; set; }

        public string? Price { get; set; }

        public string? SaleTotal { get; set; }

        public string? Total { get; set; }
        
    }
}
