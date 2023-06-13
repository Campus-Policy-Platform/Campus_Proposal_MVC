using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHU_PolicyPlatform_1.ViewModels;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProposeContext _prop;

        
        public HomeController(ProposeContext prop)
        {
            _prop = prop;
        }
        //[HttpGet]
        public IActionResult Index()
        {
            var props = _prop.Proposals;
            var votes = _prop.Votes;

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = props.ToList(),
                Votes = votes.ToList(),
            };

            return View(scanVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
