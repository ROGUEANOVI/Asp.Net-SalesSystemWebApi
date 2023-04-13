
using SalesSystemWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetList();
        
        Task<SessionDTO> CredentialValidation(string email, string password);

        Task<UserDTO> Create(UserDTO entity);

        Task<bool> Update(UserDTO entity);

        Task<bool> Delete(int id);
    }
}
