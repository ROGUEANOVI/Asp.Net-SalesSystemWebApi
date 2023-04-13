using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystemWebApi.BLL.Services.Interface;
using SalesSystemWebApi.DAL.Repositories.Interface;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services
{
    public class SaleService : ISaleService
    {

        private ISaleRepository _saleRepository;

        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;

        private readonly IMapper _mapper;


        public SaleService(ISaleRepository saleRepository, IGenericRepository<SaleDetail> saleDetailRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDTO>> GetList()
        {
            try
            {
                var saleQuery = await _saleRepository.Consult();
                var salesList = saleQuery.Include(p => p.SaleDetails).ToList();

                var salesListDTO = _mapper.Map<List<SaleDTO>>(salesList);

                return salesListDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SaleDTO> Register(SaleDTO entity)
        {
            try
            {
                var sale = _mapper.Map<Sale>(entity);
                var saleGenerated = await _saleRepository.Register(sale);

                if (saleGenerated == null)
                {
                    throw new TaskCanceledException("¡NO se pudo generar la venta!");
                }

                entity = _mapper.Map<SaleDTO>(saleGenerated);
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> GetReport(string initialDate, string lastDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.Consult();
            var resultList = new List<SaleDetail>();

            try
            {
                DateTime initial = DateTime.ParseExact(initialDate, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime last = DateTime.ParseExact(lastDate, "dd/MM/yyyy", new CultureInfo("es-CO"));

                resultList = await query
                    .Include(sd => sd.Product)
                    .Include(sd => sd.Sale)
                    .Where(sd =>
                        sd.Sale.RegistrationDate.Value.Date >= initial.Date &&
                        sd.Sale.RegistrationDate.Value.Date <= last.Date
                    ).ToListAsync();

            }
            catch (Exception ex)
            {
                throw;
            }

            var resultListDTO = _mapper.Map<List<ReportDTO>>(resultList);
            
            return resultListDTO;
        }

        public async Task<List<SaleDTO>> History(string searchBy, string saleNumber, string initialDate, string lastDate)
        {
            IQueryable<Sale> query = await _saleRepository.Consult();
            var resultList = new List<Sale>();
            try
            {
                if (searchBy == "date")
                {
                    DateTime initial = DateTime.ParseExact(initialDate, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime last = DateTime.ParseExact(lastDate, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    resultList = await query.Where(s => 
                        s.RegistrationDate.Value.Date >= initial.Date && 
                        s.RegistrationDate.Value.Date <= last.Date

                    ).Include(v => v.SaleDetails).ThenInclude(s => s.Product).ToListAsync();
                }
                else
                {
                    resultList = await query.Where(s => s.SaleTicketNumber == saleNumber)
                                            .Include(v => v.SaleDetails)
                                            .ThenInclude(s => s.Product)
                                            .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            var resultListDTO = _mapper.Map<List<SaleDTO>>(resultList);

            return resultListDTO;
        }

    }
}
