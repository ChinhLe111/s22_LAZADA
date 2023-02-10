using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebLazadaMvc.Models;

namespace WebLazadaMvc.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<MVCAccount> Model;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Account").Result;
            Model = response.Content.ReadAsAsync<IEnumerable<MVCAccount>>().Result;
            return View(Model);
        }
    }
}
