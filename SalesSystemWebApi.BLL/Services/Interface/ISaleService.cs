using SalesSystemWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services.Interface
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> GetList();

        Task<SaleDTO> Register(SaleDTO entity);
        
        Task<List<SaleDTO>> History(string searchBy, string saleNumber, string initialDate, string lastDate);
        
        Task<List<ReportDTO>> GetReport(string initialDate, string lastDate);
    }
}
