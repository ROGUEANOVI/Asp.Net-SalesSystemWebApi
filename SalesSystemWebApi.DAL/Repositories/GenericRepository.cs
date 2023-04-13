using Microsoft.EntityFrameworkCore;
using SalesSystemWebApi.DAL.DBContext;
using SalesSystemWebApi.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SalesSystemDbContext _dbContext;

        DbSet<T> _EntitySet;

        public GenericRepository(SalesSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _EntitySet = dbContext.Set<T>();
        }


        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                T entity = await _EntitySet.FirstOrDefaultAsync(filter);

                return entity;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                _EntitySet.Add(entity);

                await _dbContext.SaveChangesAsync();

                return entity;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _EntitySet.Update(entity);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _EntitySet.Remove(entity);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IQueryable<T>> Consult(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                IQueryable<T> entityQuery = filter == null ? _EntitySet : _EntitySet.Where(filter);

                return entityQuery;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
