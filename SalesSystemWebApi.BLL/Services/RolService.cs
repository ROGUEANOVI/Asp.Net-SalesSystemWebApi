using AutoMapper;
using SalesSystemWebApi.BLL.Services.Interface;
using SalesSystemWebApi.DAL.Repositories.Interface;
using SalesSystemWebApi.DTO;
using SalesSystemWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.BLL.Services
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;

        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }


        public async Task<List<RolDTO>> GetList()
        {
            try
            {
                var rolsList = await _rolRepository.Consult();

                var rolsListDTO = _mapper.Map<List<RolDTO>>(rolsList.ToList());

                return rolsListDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
