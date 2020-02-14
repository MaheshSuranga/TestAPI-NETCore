using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAppAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PagesController : Controller
    {
        public async Task<ViewResult> RegisterPage()
        {
            return View("~/Views/registerPage.cshtml");
        }
        public async Task<ViewResult> TestPage()
        {
            return View("~/Views/test.html");
        }
    }
}
