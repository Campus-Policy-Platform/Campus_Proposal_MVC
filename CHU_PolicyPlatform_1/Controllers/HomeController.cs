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
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class HomeController : Controller
    {
        private int pages;
        private readonly ProposeContext _prop;
        private readonly ProposalService _propService;

        public HomeController(ProposeContext prop, ProposalService propService)
        {
            _prop = prop;
            _propService = propService;            
        }
        [Authorize(Roles = "User,Gerent")]
        //[HttpGet]
        public async Task<IActionResult> Index(int Id=1)
        {
            if (!User.IsInRole("User"))
            {
                return RedirectToAction("GerentSee", "Gerentcase");
            }
            List<Proposal> props = setPages(Id, await _propService.readProposal());
            var votes = _prop.Votes;

            if (Id > pages || Id < 1)
            {
                return RedirectToAction("Errorhomepage", "Home");
            }

            var cateList = _prop.Categories.ToList();

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = pages;  //頁數

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = props,
                Votes = votes.ToList(),
            };

            return View(scanVM);
        }

        //Search
        [Authorize(Roles = "User")]
        [HttpGet, HttpPost]
        public async Task<IActionResult> Privacy(string keyword, int Id=1)
        {
            List<Proposal> props = setPages(Id, await _propService.readProposal(), keyword);
            var votes = _prop.Votes;

            if (Id > pages || Id < 1)
            {
                return RedirectToRoute("Failurepage");
            }
            else if (props == null) 
            {
                ViewData["NotFind"] = "查無結果!!";
            }            

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = pages;  //頁數
            ViewData["Keyword"] = keyword;

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = props,
                Votes = votes.ToList(),
            };

            return View(scanVM);
        }

        //頁碼
        public List<Proposal> setPages(int Id, List<Proposal> props, string keyword=null)
        {
            int totalRows = -1;
            props = props.FindAll(e => e.Underways == true);

            if (keyword != null)
            {
                //props中，包含keyword的資料
                List<Proposal> proposals = new List<Proposal>();
                List<Proposal> proposalList = new List<Proposal>();
                var words = keyword.Split(' ');
                
                for(var i = 0; i < words.Count(); i++)
                {
                    if (i < 1)
                    {
                        foreach (var p in props)
                        {
                            bool containT = p.Title.ToLower().Contains(words[0].ToLower());
                            bool containC = p.Pcontent.ToLower().Contains(words[0].ToLower());
                            bool containGI = p.GainsInfluential.ToLower().Contains(words[0].ToLower());
                            if (containT || containC || containGI) { proposals.Add(p); }
                        }
                    }
                    else
                    {
                        proposalList.Clear();
                        foreach (var p in proposals)
                        {
                            bool containT = p.Title.ToLower().Contains(words[i].ToLower());
                            bool containC = p.Pcontent.ToLower().Contains(words[i].ToLower());
                            bool containGI = p.GainsInfluential.ToLower().Contains(words[i].ToLower());
                            if (containT || containC || containGI) 
                            { 
                                proposalList.Add(p); 
                            }
                        }
                        proposals.Clear();
                        proposals.AddRange(proposalList);
                    }
                }
                props.Clear();
                props.AddRange(proposals);
            }            

            if (totalRows == -1)
            {
                totalRows = props.Count();   //計算總筆數
            }
            int activePage = Id; //目前所在頁
            int pageRows = 8;   //每頁幾筆資料

            //計算Page頁數
            if (totalRows % pageRows == 0)
            {
                pages = totalRows / pageRows;
            }
            else
            {
                pages = (totalRows / pageRows) + 1;
            }

            int startRow = (activePage - 1) * pageRows;  //起始記錄Index
            List<Proposal> products = props.Skip(startRow).Take(pageRows).ToList();

            return products;
        }
        [Authorize(Roles = "User")]
        public IActionResult Variousrooms()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Failurepage()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Errorhomepage()
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
