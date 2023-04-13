using AutoMapper;
using SalesSystemWebApi.BLL.Services.Interface;
using SalesSystemWebApi.DAL.Repositories.Interface;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IGenericRepository<Product> _productRepository;

        private readonly IMapper _mapper;

        private ISaleRepository _saleRepository;

        public DashboardService(IGenericRepository<Product> productRepository, IMapper mapper, ISaleRepository saleRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        private IQueryable<Sale> SalesReturn(IQueryable<Sale> saleTable, int subtractDaysAmount)
        {
            DateTime? lastDate = saleTable.OrderByDescending(v => v.RegistrationDate).Select(v => v.RegistrationDate).First();
            lastDate = lastDate.Value.AddDays(subtractDaysAmount);

            return saleTable.Where(v => v.RegistrationDate.Value.Date >= lastDate.Value.Date);
        }   

        private async Task<int> TotalSalesLastWeek()
        {
            int total = 0;
            IQueryable<Sale> saleQuery = await _saleRepository.Consult();

            if (saleQuery.Count() > 0)
            {

                var saleTable = SalesReturn(saleQuery, -7);
                total = saleTable.Count();
            }

            return total;
        }

        private async Task<string> TotalIncomesLastWeek()
        {
            decimal result = 0;
            IQueryable<Sale> saleQuery = await _saleRepository.Consult();

            if (saleQuery.Count() > 0)
            {
                var saleTable = SalesReturn(saleQuery, -7);
                result = saleTable.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(result, new CultureInfo("es-CO"));
        }

        private async Task<int> ProductsTotal()
        {
            IQueryable<Product> productQuery =  await _productRepository.Consult();
            int total = productQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> SalesLastWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            IQueryable<Sale> ventaQuery = await _saleRepository.Consult();

            if (ventaQuery.Count() > 0)
            {
                var saleTable = SalesReturn(ventaQuery, -7);
                result = saleTable.GroupBy(v => v.RegistrationDate.Value.Date)
                                  .OrderBy(v => v.Key)
                                  .Select(v => new { date = v.Key.ToString("dd/MM/yyyy"), total = v.Count() })
                                  .ToDictionary(keySelector: k => k.date, elementSelector: e => e.total);
                
            }

            return result;
        }


        public async Task<DashboardDTO> Resume()
        {
            var vmDashboard = new DashboardDTO();


            try
            {
                vmDashboard.SalesTotal = await TotalSalesLastWeek();
                vmDashboard.IncomesTotal = await TotalIncomesLastWeek();
                vmDashboard.ProductsTotal = await ProductsTotal();

                List<WeekSalesDTO> weekSalesList = new List<WeekSalesDTO>();

                foreach (KeyValuePair<string, int> item in await SalesLastWeek())
                {
                    weekSalesList.Add(new WeekSalesDTO() {
                        Date = item.Key,
                        Total = item.Value,
                    });
                }

                vmDashboard.LastWeekSales = weekSalesList;
            }
            catch (Exception ex)
            {
                throw;
            }

            return vmDashboard;
        }
    }
}
