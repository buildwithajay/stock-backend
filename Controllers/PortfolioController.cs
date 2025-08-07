using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extesnsions;
using api.Interfaces;
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
        private readonly IPortfolioRepository _porfolioRepo;
        public PortfolioController(UserManager<AppUser> user, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _user = user;
            _stockRepo = stockRepo;
            _porfolioRepo = portfolioRepo;

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
            var userPortfolio = await _porfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

    }
}