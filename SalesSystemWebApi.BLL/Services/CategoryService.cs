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
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetList()
        {
            try
            {
                var categoriesList = await _categoryRepository.Consult();
                var categoriesListDTO = _mapper.Map<List<CategoryDTO>>(categoriesList.ToList());
                return categoriesListDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
