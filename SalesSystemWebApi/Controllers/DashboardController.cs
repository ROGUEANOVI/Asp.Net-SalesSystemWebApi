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
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("Resume")]
        public async Task<IActionResult> Resume()
        {
            var response = new Response<DashboardDTO>();

            try
            {
                response.Value = await _dashboardService.Resume();
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
