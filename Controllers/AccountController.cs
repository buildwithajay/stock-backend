using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers
{
    public class AccountController
    {
        private readonly UserManager<AppUser> _user;
        public AccountController(UserManager<AppUser> user)
        {
            _user = user;
        }
    }
}