using SalesSystemWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetList();

        Task<ProductDTO> Create(ProductDTO entity);

        Task<bool> Update(ProductDTO entity);

        Task<bool> Delete(int id);
    }
}
