using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository

    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Stock> Create()
        {
            throw new NotImplementedException();
        }

        public Task<Stock?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.stocks.ToListAsync();
        }

        public Task<Stock?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock?> Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}