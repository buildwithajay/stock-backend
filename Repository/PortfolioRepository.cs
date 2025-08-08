using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeleteAync(AppUser appUser, string Symbol)
        {
            var portfolioModel = await _context.portfolios.FirstOrDefaultAsync(s => s.AppUserId == appUser.Id && s.Stock.Symbol.ToLower() == Symbol.ToLower());
            if (portfolioModel == null)
            {
                return null;
            }
            _context.portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

       

        public Task<List<Stock>> GetUserPortfolio(AppUser user)
        { 

            return _context.portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.stockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }
    }
}