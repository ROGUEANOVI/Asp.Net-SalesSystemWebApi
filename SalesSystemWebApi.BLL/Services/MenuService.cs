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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int userId)
        {
            IQueryable<User> tbUser = await _userRepository.Consult(u => u.UserId == userId);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepository.Consult();
            IQueryable<Menu> tbMenu = await _menuRepository.Consult();

            try
            {
                IQueryable<Menu> tbResult = (from u in tbUser
                                             join mr in tbMenuRol on u.RolId equals mr.RolId
                                             join m in tbMenu on mr.MenuId equals m.MenuId
                                             select m).AsQueryable();
                
                var menusList = tbResult.ToList();

                var menusListDTO = _mapper.Map<List<MenuDTO>>(menusList);

                return menusListDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
