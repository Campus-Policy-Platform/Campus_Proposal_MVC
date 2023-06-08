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
            var content = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Doloribus illo praesentium quibusdam illum, rem sequi atque, officia repudiandae modi cupiditate excepturi explicabo ullam et cum sint esse tempora impedit quia?";
            List<Proposal> proposals = new List<Proposal>
            {
                new Proposal {ProposalId = "P001", Title="aaa", Pcontent=content, CategoryId="C001"},
                new Proposal {ProposalId = "P002", Title="bba", Pcontent=content, CategoryId= "C004"},
                new Proposal {ProposalId = "P003", Title="ccc", Pcontent = content, CategoryId = "C002"},
                new Proposal {ProposalId = "P004", Title="ddd", Pcontent = content, CategoryId = "C002"},
                new Proposal {ProposalId = "P005", Title="eee", Pcontent = content, CategoryId = "C003"},
                new Proposal {ProposalId = "P006", Title="111", Pcontent = content, CategoryId = "C003"},
                new Proposal {ProposalId = "P007", Title="222", Pcontent = content, CategoryId = "C001"},
                new Proposal {ProposalId = "P008", Title="333", Pcontent = content, CategoryId = "C004"},
                new Proposal {ProposalId = "P009", Title="444", Pcontent = content, CategoryId = "C001"}
            };
            var props = _prop.Proposals;
            var votes = _prop.Votes;
            
            ViewData["proposal"] = proposals;
            ViewData["vote"] = votes;

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
