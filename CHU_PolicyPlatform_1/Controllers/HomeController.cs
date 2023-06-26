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
            List<Proposal> props = setPages(Id, await _propService.readProposal());
            var votes = _prop.Votes;

            if (Id > pages || Id < 1)
            {
                return NotFound();
            }

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
        [Authorize(Roles = "User,Gerent")]
        [HttpGet, HttpPost]
        public async Task<IActionResult> Privacy(string keyword, int Id=1)
        {
            List<Proposal> props = setPages(Id, await _propService.readProposal(), keyword);
            var votes = _prop.Votes;

            if (Id > pages || Id < 1)
            {
                return NotFound();
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
                foreach (var p in props)
                {
                    bool containT = p.Title.ToLower().Contains(keyword.ToLower());
                    bool containC = p.Pcontent.ToLower().Contains(keyword.ToLower());
                    bool containGI = p.GainsInfluential.ToLower().Contains(keyword.ToLower());
                    if (containT || containC || containGI) { proposals.Add(p); }
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
            List<Proposal> products = props.OrderBy(x => x.ProposalId).Skip(startRow).Take(pageRows).ToList();

            return products;
        }

        public IActionResult Variousrooms()
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
