using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DAL.Repositories.Interface
{
    public interface IGenericRepository<T>  where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<IQueryable<T>> Consult(Expression<Func<T, bool>> filter = null);
    }
}
