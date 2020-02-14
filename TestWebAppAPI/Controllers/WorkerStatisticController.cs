using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAppAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class WorkerStatisticController : Controller
    {
        [HttpGet]
        public async Task<JsonResult> GetDetail()
        {
            if (await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return null;
            }
            Response.Headers.Add("auth", "1");
            string codetosearch = Request.Query["code"];
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);

            dynamic mainoutput = null;
            var output = await MainLibrary.WorkerStatistic.FindWorkerDataByIdAsync(codetosearch, ownercode);
            string searchtype = Request.Query["type"];

            if (searchtype == "2")
            {
                mainoutput = output.hourlyrate * output.hoursworked;
            }
            else if (searchtype == "3")
            {
                mainoutput = output.overtimerate * output.overtimeworked;
            }
            else
            {
                mainoutput = output;
            }
            return Json(new { mainoutput });
        }
        [HttpPut]
        public async Task<bool> UpdateDetail()
        {
            if (await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return false;
            }
            Response.Headers.Add("auth", "1");
            string codetosearch = Request.Query["code"];
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);

            double hourlyrate = 0;
            double hoursworked = 0;
            double overtimerate = 0;
            double overtimeworked = 0;

            if(Request.Query["H_rate"] == "1")
            {
                hourlyrate = Convert.ToDouble(Request.Form["hrate"]);
            }
            if (Request.Query["H_worked"] == "1")
            {
                hourlyrate = Convert.ToDouble(Request.Form["hworked"]);
            }
            if (Request.Query["O_rate"] == "1")
            {
                hourlyrate = Convert.ToDouble(Request.Form["orate"]);
            }
            if (Request.Query["O_worked"] == "1")
            {
                hourlyrate = Convert.ToDouble(Request.Form["oworked"]);
            }

            return await MainLibrary.WorkerStatistic.UpdateWorkerStatisticAsync(codetosearch, hourlyrate, hoursworked,
                overtimerate, overtimeworked, ownercode);
        }
    }
}
