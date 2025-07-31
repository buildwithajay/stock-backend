using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.stocks.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.stocks.Include(c=>c.comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockModel = await _context.stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            return stockModel;
        }
        

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStock)
        {
            var stockModel = await _context.stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = updateStock.Symbol;
            stockModel.CompanyName = updateStock.CompanyName;
            stockModel.Purchase = updateStock.Purchase;
            stockModel.LastDiv = updateStock.LastDiv;
            stockModel.Industry = updateStock.Industry;
            stockModel.MarketCap = updateStock.MarketCap;
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}