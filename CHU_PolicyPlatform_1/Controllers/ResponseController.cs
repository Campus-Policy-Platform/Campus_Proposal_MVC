using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Services;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ProposeContext _context;
        public ResponseController(ProposeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Gerent")]
        [HttpGet]
        public IActionResult GerentResponse(string propId)
        {
            //string propId= "P230608001"
            string gerentId = User.Identity.Name;


            var proposal = _context.Proposals.ToList().Find(e => e.ProposalId == propId);

            ResponseViewModel responseVM = new ResponseViewModel
            {
                proposal = proposal,
                toRepond = new ToRepond { ProposalId = proposal.ProposalId, GerentId = gerentId }
            };
            return View(responseVM);
        }
        [Authorize(Roles = "Gerent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GerentResponse(ToRepond toRepond)
        {
            //資料庫屬性為text(不可中文)，用unicode編譯語言存進資料庫
            toRepond.Rcontent = String2Unicode(toRepond.Rcontent);

            if (ModelState.IsValid)
            {
                _context.ToReponds.Add(toRepond);
                _context.SaveChangesAsync();
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("GerentSee", "Gerentcase");
            }
            return View(toRepond);
        }

        /// 字符串转Unicode
        /// 源字符串
        /// Unicode编码后的字符串
        internal static string String2Unicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
    }
}
