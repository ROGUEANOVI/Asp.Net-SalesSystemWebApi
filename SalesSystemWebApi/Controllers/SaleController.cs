using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystemWebApi.BLL.Services;
using SalesSystemWebApi.BLL.Services.Interface;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Shared;

namespace SalesSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<SaleDTO>>();

            try
            {
                response.Value = await _saleService.GetList();
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> GetHistory(string searchBy, string? saleNumber, string? initialDate, string? lastDate)
        {
            var response = new Response<List<SaleDTO>>();
            
            saleNumber = saleNumber is null ? "" : saleNumber;
            initialDate = initialDate is null ? "" : initialDate;
            lastDate = lastDate is null ? "" : lastDate;

            try
            {
                response.Value = await _saleService.History(searchBy, saleNumber, initialDate, lastDate);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> GetReport(string? initialDate, string? lastDate)
        {
            var response = new Response<List<ReportDTO>>();

            try
            {
                response.Value = await _saleService.GetReport(initialDate, lastDate);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] SaleDTO sale)
        {
            var response = new Response<SaleDTO>();

            try
            {
                response.Value = await _saleService.Register(sale);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }
    }
}
