using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _context.stocks.ToListAsync();
            var stockDtos= stock.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock =await _context.stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateStockRequestDto stock)
        {
            var item = await _context.stocks.FirstOrDefaultAsync(item => item.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            item.Symbol = stock.Symbol;
            item.CompanyName = stock.CompanyName;
            item.Purchase = stock.Purchase;
            item.LastDiv = stock.LastDiv;
            item.Industry = stock.Industry;
            item.MarketCap = stock.MarketCap;
                    _context.Update(item);
            await _context.SaveChangesAsync();
            return Ok(item.ToStockDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock =await _context.stocks.FirstOrDefaultAsync(stocks => stocks.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.stocks.Remove(stock);

           await _context.SaveChangesAsync();
            return NoContent();
        }

    }

  
  

    
}