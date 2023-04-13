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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList(int userId)
        {
            var response = new Response<List<MenuDTO>>();

            try
            {
                response.Value = await _menuService.List(userId);
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
