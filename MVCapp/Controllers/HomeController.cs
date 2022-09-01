using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCapp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PagedList;

namespace MVCapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index(string Search)
        {
            IndexViewModel vm = new IndexViewModel();
            List<User> persons = unitOfWork.UserRepository.GetAll().OfType<User>().ToList();

          vm.Persons = persons;

            if (!string.IsNullOrEmpty(Search))
            {
                vm.Persons = unitOfWork.UserRepository.SearchByFirstLastName(Search).OfType<User>().ToList();
            }
            // vm.Persons = unitOfWork.UserRepository.SearchByFirstLastName(Search).ToList();

            return View(vm);
        }



        public IActionResult Privacy()
        {
            return View();
        }

       public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
       

        [HttpGet]
        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var content = await client.GetAsync("http://localhost:5004/api/Identity/identity");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5004");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = client.GetAsync("api/Identity/identity").Result;
                string res = "";
                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;

                    return Json(res);
                }
            }

            //
            //return View("json");
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
