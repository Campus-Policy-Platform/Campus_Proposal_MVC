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
using CHU_PolicyPlatform_1.Services;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProposeContext _prop;
        private readonly ProposalService _propService;
        public HomeController(ProposeContext prop, ProposalService propService)
        {
            _prop = prop;
            _propService = propService;
        }
        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var props = await _propService.readProposal();
            var votes = _prop.Votes;

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = props.FindAll(e=>e.Underways==true),
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
