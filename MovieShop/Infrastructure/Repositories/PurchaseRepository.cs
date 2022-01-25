using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository: EfRepository<Purchase>, IPurchaseRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Purchase>>  GetByUserId(int userId)
        {
            var purchases = await _dbContext.Purchases.Where(x => x.UserId == userId).Include(m=>m.Movie)
                .ToListAsync();
            return purchases;
        }
        public async Task<int> GetNumberOfPurchases()
        {
            var num = await _dbContext.Purchases.CountAsync();
            return num;
        }
        public async Task<List<Purchase>> GetPurchasesinRange(DateTime x, DateTime y)
        {
            var toppurchases = await _dbContext.Purchases.Where(p => p.PurchaseDateTime > x && p.PurchaseDateTime < y).Include(m => m.Movie).ToListAsync();
            return toppurchases;
            

        }




    }
}
