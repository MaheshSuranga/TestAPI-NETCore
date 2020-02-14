using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAppAPI.Controllers
{
    [Route("api/[controller]")]
    public class WorkerController : Controller
    {
        [HttpGet]
        public async Task<MainLibrary.Worker.WorkerData> GetWorker()
        {
            if(await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return null;
            }
            Response.Headers.Add("auth", "1");
            string codetosearch = Request.Query["code"];
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);
            return await MainLibrary.Worker.FindWorkerDataByIdAsync(codetosearch, ownercode);
        }
        [HttpPost]
        public async Task<string> CreateNew()
        {
            if (await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return null;
            }
            Response.Headers.Add("auth", "1");
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);

            return await MainLibrary.Worker.CreateNewAsync(Request.Form["name"], Request.Form["description"],
                Request.Form["location"], Request.Form["position"], ownercode);
        }
        [HttpPut]
        public async Task<bool> Update()
        {
            if (await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return false;
            }
            Response.Headers.Add("auth", "1");
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);

            string codetosearch = Request.Query["code"];

            return await MainLibrary.Worker.UpadteWorkerAsync(codetosearch, Request.Form["name"], Request.Form["description"],
                Request.Form["location"], Request.Form["position"], ownercode);
        }
        [HttpDelete]    
        public async Task<bool> Remove()
        {
            if (await AccountsLibrary.Authorization.VerifyToken(Request.Headers["auth"]) != true)
            {
                Response.Headers.Add("auth", "0");
                return false;
            }
            Response.Headers.Add("auth", "1");
            string ownercode = await AccountsLibrary.Authorization.RetrieveId(Request.Headers["auth"]);

            string codetosearch = Request.Query["code"];

            return await MainLibrary.Worker.DeleteWorkerAsync(codetosearch, ownercode);
        }
    }
}
