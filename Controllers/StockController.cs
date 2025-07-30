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
        public IActionResult GetAll()
        {
            var stock = _context.stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stock);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            _context.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, UpdateStockRequestDto stock)
        {
            var item = _context.stocks.FirstOrDefault(item => item.Id == id);
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
            _context.SaveChanges();
            return Ok(item.ToStockDto());
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _context.stocks.FirstOrDefault(stocks => stocks.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.stocks.Remove(stock);

            _context.SaveChanges();
            return NoContent();
        }

    }

  
  

    
}