using CHU_PolicyPlatform_1.Data;
using Microsoft.AspNetCore.Mvc;
using CHU_PolicyPlatform_1.ViewModels;
using CHU_PolicyPlatform_1.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        [Authorize(Roles = "User,Gerent")]
        [HttpGet]
        public IActionResult Review_Interface(string Pro_Id, int Id=1)
        {
            var review_data = _context.Proposals.ToList().Find(z => z.ProposalId == Pro_Id);
            var review_vote = _context.Votes.ToList().FindAll(x => x.ProposalId == Pro_Id);
            var review_response = _context.ToReponds.ToList().Find(c => c.ProposalId == Pro_Id);

            //Unicode轉回中文字串
            if(review_response!=null)
            {
                review_response.Rcontent = Unicode2String(review_response.Rcontent);
            }

            //True and False 數量統計
            var TrueAmount = review_vote.Count(h => h.Crucial == true);
            var FalseAmount = review_vote.Count(h => h.Crucial == false);

            ViewBag.TrueAmount = TrueAmount;
            ViewBag.FalseAmount = FalseAmount;

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

            bool exist = false;
            foreach (var vote in review_vote)
            {
                if (vote.ProposalId == review_data.ProposalId && vote.UserId == User.Identity.Name)
                {
                    exist = true;
                }
            }

            ViewData["ActivePage"] = Id;    //Activec分頁碼
            ViewData["Pages"] = pages;  //頁數
            ViewData["Voted"] = exist;  //是否已投票


            return View(ReviewtotalVM);
        }
        [Authorize(Roles = "Gerent")]
        [HttpPost]
        public  IActionResult Delete_Response(string propId) 
        {
            //string propId = "P230523001";
            var review_responses = _context.ToReponds.Where(c => c.ProposalId == propId).FirstOrDefault();
            if (review_responses != null)
            {
                _context.ToReponds.Remove(review_responses);
                _context.SaveChanges();
            }
            else 
            {
                return NotFound();
            }

            return RedirectToAction("GerentSee", "Gerentcase");
        }



        public List<Vote> setPages(int Id, List<Vote> props)
        {
            int totalRows = -1;


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
            List<Vote> products = props.OrderByDescending(e=>e.Vdate).Skip(startRow).Take(pageRows).ToList();

            return products;
        }


        /// Unicode转字符串
        /// 经过Unicode编码的字符串
        /// 正常字符串
        internal static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
        }
    }
}
