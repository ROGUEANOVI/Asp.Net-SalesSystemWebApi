using SalesSystemWebApi.DAL.DBContext;
using SalesSystemWebApi.DAL.Repositories.Interface;
using SalesSystemWebApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DAL.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly SalesSystemDbContext _dbContext;

        public SaleRepository(SalesSystemDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> Register(Sale entity)
        {
            await Console.Out.WriteLineAsync(entity.SaleDetails.ToList().ToString());
            Sale SaleGerated = new Sale();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (SaleDetail saleDetail in entity.SaleDetails)
                    {
                        Product productFound = _dbContext.Products.Where(p => p.ProductId == saleDetail.ProductId).First();
                        productFound.Stock = productFound.Stock - saleDetail.Amount;
                        _dbContext.Products.Update(productFound);
                    }

                    await _dbContext.SaveChangesAsync();

                    NumberTicket corralative = _dbContext.NumberTickets.First();
                    corralative.LastNumber = corralative.LastNumber + 1;
                    corralative.RegistrationDate = DateTime.Now;

                    _dbContext.NumberTickets.Update(corralative);
                    await _dbContext.SaveChangesAsync();

                    int digitAmount = 4;
                    string zeros = string.Concat(Enumerable.Repeat("0", digitAmount));
                    string saleNumber = zeros + corralative.LastNumber.ToString();

                    saleNumber = saleNumber.Substring(saleNumber.Length - digitAmount, digitAmount);

                    entity.SaleTicketNumber = saleNumber;

                    await _dbContext.Sales.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();

                    SaleGerated = entity;

                    transaction.Commit();

                }   
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

                return SaleGerated;
            }
        }
    }   
}
