using SalesSystemWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services.Interface
{
    public interface IRolService
    {
        Task<List<RolDTO>> GetList();
    }
}
