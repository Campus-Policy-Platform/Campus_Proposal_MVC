using CHU_PolicyPlatform_1.Data;
using Microsoft.AspNetCore.Mvc;
using CHU_PolicyPlatform_1.ViewModels;
using CHU_PolicyPlatform_1.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ProposeContext _context;
        private  int pages;
        public ReviewController(ProposeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Review_Interface(string Pro_Id, int Id=1)
        {
            var review_data = _context.Proposals.ToList().Find(z => z.ProposalId == Pro_Id);
            var review_vote = _context.Votes.ToList().FindAll(x => x.ProposalId == Pro_Id);
            var review_response = _context.ToReponds.ToList().Find(c => c.ProposalId == Pro_Id);
            ReviewtotalViewModel ReviewtotalVM = new ReviewtotalViewModel()
            {
                proposal = review_data,
                votes=setPages(Id, review_vote),
                toRepond =review_response
            };

  

            if ( review_vote.Count>0 &&(Id > pages || Id < 1))
            {
                return NotFound();
            }

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = pages;  //頁數


            return View(ReviewtotalVM);
        }

        public List<Vote> setPages(int Id, List<Vote> props)
        {
            int totalRows = -1;


            if (totalRows == -1)
            {
                totalRows = props.Count();   //計算總筆數
            }
            int activePage = Id; //目前所在頁
            int pageRows = 1;   //每頁幾筆資料

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
            List<Vote> products = props.Skip(startRow).Take(pageRows).ToList();

            return products;
        }
    }
}
