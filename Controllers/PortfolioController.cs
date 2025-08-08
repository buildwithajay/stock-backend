using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extesnsions;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _user;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> user, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _user = user;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            if (username == null)
            {
                return BadRequest("username not provided");
            }
            var appUser = await _user.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserPorfolio(string Symbol)
        {
            var username = User.GetUsername();
            var appUser = await _user.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var stock = await _stockRepo.GetBySymbolAsync(Symbol);
            if (stock == null) return BadRequest("stock not found");

            if (userPortfolio.Any(e => e.Symbol.ToLower() == Symbol.ToLower())) return BadRequest("cannot add the same stock in the porfolio");
            var porfolioModel = new Portfolio
            {
                stockId = stock.Id,
                AppUserId = appUser.Id
            };
            await _portfolioRepo.CreateAsync(porfolioModel);
            if (porfolioModel == null) return StatusCode(500, "cannot created");
            else return Created();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string Symbol)
        {
            var username = User.GetUsername();
            var appUser = await _user.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == Symbol.ToLower());
            if (filteredStock.Count() == 1)
            {
                await _portfolioRepo.DeleteAync(appUser, Symbol);
            }
            else
            {
                return BadRequest("stock not found");
            }
            return Ok();

        }

    }
}