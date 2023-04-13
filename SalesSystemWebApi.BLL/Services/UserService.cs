using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<List<UserDTO>> GetList()
        {
            try
            {
                var userQuery = await _userRepository.Consult();
                var usersList = userQuery.Include(u => u.Rol).ToList();

                var usersListDTO = _mapper.Map<List<UserDTO>>(usersList);

                return usersListDTO;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<SessionDTO> CredentialValidation(string email, string password)
        {
            try
            {
                var userQuery = await _userRepository.Consult(u => 
                    u.Email == email && u.Password == password
                );

                if (userQuery.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("¡Este usuario no esta registrado!");
                }

                User user = userQuery.Include(u => u.Rol).First();

                var sessionDTO = _mapper.Map<SessionDTO>(user);

                return sessionDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userCreated = await _userRepository.Add(user);

                if (userCreated.UserId == 0)
                {
                    throw new TaskCanceledException("¡Este usuario NO se pudo crear!");
                }

                var query = await _userRepository.Consult(u => u.UserId == userCreated.UserId);
                userCreated = query.Include(u => u.Rol).First();

                userDTO = _mapper.Map<UserDTO>(userCreated);

                return userDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var userFound = await _userRepository.Get(u => u.UserId == user.UserId);

                if (userFound == null)
                {
                    throw new TaskCanceledException("¡Este usuario no esta registrado!");
                }

                userFound.FullName = user.FullName;
                userFound.Email = user.Email;
                userFound.Password = user.Password;
                userFound.IsActive = user.IsActive;
                userFound.RolId = user.RolId;

                bool resopnse = await _userRepository.Update(userFound);
                
                if (!resopnse)
                {
                    throw new TaskCanceledException("¡No se pudo actualizar el usuario!");
                }

                return resopnse;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFound = await _userRepository.Get(u => u.UserId == id);
                if (userFound == null)
                {
                    throw new TaskCanceledException("¡Este usuario no esta registrado!");
                }

                bool response = await _userRepository.Delete(userFound);

                if (!response)
                {
                    throw new TaskCanceledException("¡No se pudo eliminar el usuario!");
                }

                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
