using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystemWebApi.BLL.Services.Interface;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Shared;

namespace SalesSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<RolDTO>>();

            try
            {
                response.Value = await _rolService.GetList();
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message =ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }
    }
}
