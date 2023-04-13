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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<ProductDTO>>();

            try
            {
                response.Value = await _productService.GetList();
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
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] ProductDTO product)
        {
            var response = new Response<ProductDTO>();

            try
            {
                response.Value = await _productService.Create(product);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }


        [HttpPut]
        [Route("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([FromBody] ProductDTO product)
        {
            var response = new Response<bool>();

            try
            {
                response.Value = await _productService.Update(product);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                response.Value = await _productService.Delete(id);
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
