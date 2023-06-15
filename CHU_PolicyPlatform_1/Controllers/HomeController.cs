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
        public async Task<IActionResult> Index(int Id=1)
        {
            int totalRows = -1;
            var props = await _propService.readProposal();
            props = props.FindAll(e => e.Underways == true);
            var votes = _prop.Votes;
            
            if (totalRows == -1)
            {
                totalRows = props.Count();   //計算總筆數
            }
            int activePage = Id; //目前所在頁
            int pageRows = 8;   //每頁幾筆資料

            //計算Page頁數
            int Pages = 0;
            if (totalRows % pageRows == 0)
            {
                Pages = totalRows / pageRows;
            }
            else
            {
                Pages = (totalRows / pageRows) + 1;
            }

            if(Id > Pages)
            {
                return NotFound();
            }

            int startRow = (activePage - 1) * pageRows;  //起始記錄Index
            List<Proposal> products = props.OrderBy(x => x.ProposalId).Skip(startRow).Take(pageRows).ToList();

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = Pages;  //頁數

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = products,
                Votes = votes.ToList(),
            };

            return View(scanVM);
        }

        //Search
        [HttpGet, HttpPost]
        public async Task<IActionResult> Privacy(string keyword, int Id=1)
        {
            int totalRows = -1;
            var props = await _propService.readProposal();
            props = props.FindAll(e => e.Underways == true);
            var votes = _prop.Votes;

            //props中，包含keyword的資料
            List<Proposal> proposals = new List<Proposal>();
            foreach(var p in props)
            {
                bool containT = p.Title.ToLower().Contains(keyword.ToLower());
                bool containC = p.Pcontent.ToLower().Contains(keyword.ToLower());
                bool containGI = p.GainsInfluential.ToLower().Contains(keyword.ToLower());
                if (containT || containC || containGI) { proposals.Add(p); }
            }
            
            if (proposals.Count == 0) 
            {
                ViewData["NotFind"] = "查無結果!!";
            }            

            if (totalRows == -1)
            {
                totalRows = proposals.Count;   //計算總筆數
            }
            int activePage = Id; //目前所在頁
            int pageRows = 8;   //每頁幾筆資料

            //計算Page頁數
            int Pages = 0;
            if (totalRows % pageRows == 0)
            {
                Pages = totalRows / pageRows;
            }
            else
            {
                Pages = (totalRows / pageRows) + 1;
            }

            if (Id > Pages)
            {
                return NotFound();
            }

            int startRow = (activePage - 1) * pageRows;  //起始記錄Index
            List<Proposal> products = proposals.OrderBy(x => x.ProposalId).Skip(startRow).Take(pageRows).ToList();

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = Pages;  //頁數
            ViewData["Keyword"] = keyword;

            ScanViewModel scanVM = new ScanViewModel()
            {
                Proposals = products,
                Votes = votes.ToList(),
            };

            return View(scanVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
