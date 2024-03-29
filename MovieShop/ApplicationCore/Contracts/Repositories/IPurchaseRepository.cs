﻿using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<List<Purchase>> GetByUserId(int userId);
        Task<int> GetNumberOfPurchases();
        Task<List<Purchase>> GetPurchasesinRange(DateTime x, DateTime y);
    }
}
