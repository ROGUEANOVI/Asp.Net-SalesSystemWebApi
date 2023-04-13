using AutoMapper;
using Azure;
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
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetList()
        {
            try
            {
                var productQuery = await _productRepository.Consult();
                var productsList = productQuery.Include(p => p.Category).ToList();
                
                var productsListDTO = _mapper.Map<List<ProductDTO>>(productsList);

                return productsListDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductDTO> Create(ProductDTO entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                var productCreated = await _productRepository.Add(product);

                if (productCreated.ProductId == 0)
                {
                    throw new TaskCanceledException("¡No se pudo crear el producto!");
                }

                entity = _mapper.Map<ProductDTO>(product);

                return entity;
            
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductDTO entity)
        {
            try
            {
                var product = _mapper.Map<Product>(entity);
                var productFound = await _productRepository.Get(p => p.ProductId == product.ProductId);

                if (productFound == null)
                {
                    throw new TaskCanceledException("¡El producto NO esta registrado!");
                }

                productFound.Name = product.Name;
                productFound.CategoryId = product.CategoryId;
                productFound.Stock = product.Stock;
                productFound.Price = product.Price;
                productFound.IsActive = product.IsActive;

                var response = await _productRepository.Update(productFound);

                if (!response)
                {
                    throw new TaskCanceledException("¡NO se pudo editar el producto!");
                }

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var productFound = await _productRepository.Get(p => p.ProductId == id);
                if (productFound == null)
                {
                    throw new TaskCanceledException("¡El producto NO esta registrado!");
                }

                var response = await _productRepository.Delete(productFound);
                
                if (!response)
                {
                    throw new TaskCanceledException("¡NO se pudo editar el producto!");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
