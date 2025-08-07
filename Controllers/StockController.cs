using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("/api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController( IStockRepository stockRepo)
        {
       
            _stockRepo = stockRepo;
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepo.GetAllAsync(queryObject);
            var stockDtos = stock.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var stock =await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)

        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var stock = stockDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateStockRequestDto stock)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var item = await _stockRepo.UpdateAsync(id, stock);
            if (item == null)
            {
                return NotFound();
            }
            
            return Ok(item.ToStockDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            
            return NoContent();
        }

    }

  
  

    
}