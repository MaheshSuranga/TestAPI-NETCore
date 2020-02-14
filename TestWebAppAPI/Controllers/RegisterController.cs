using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAppAPI.Controllers
{
    [Route("/[controller]/[action]")]
    public class RegisterController : Controller
    {
        public async Task<string> CreateAccount(string email, string password, string entity)
        {
            string output = await AccountsLibrary.Register.CreateAsync(email, password, entity);
            return output;
        }
    }
}
