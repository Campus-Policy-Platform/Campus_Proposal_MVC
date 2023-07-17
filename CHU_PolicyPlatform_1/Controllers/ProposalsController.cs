using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace CHU_PolicyPlatform_1.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly ProposeContext _context;
        public ProposalsController(ProposeContext context) 
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Advance()
        {
            var cates = await _context.Categories.ToListAsync();
            List<CategoryOptionVM> cateOptions = cates.Select(e =>
                   new CategoryOptionVM
                   {
                       Text = $"{e.CategoryId.Substring(2)}.{e.CategoryName}",
                       Value = e.CategoryId,
                       MinDay = e.CategoryMinDay,
                       MaxDay = e.CategoryMaxDay,
                       CategoryReview = e.CategoryGerentReview
                   }).ToList();
            var model = new CategoryOptionVM
            {
                Options = cateOptions,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Advance(Proposal proposal)
        {
            
            proposal.Pdate = DateTime.Now;
            var dateNow = proposal.Pdate.ToString("yyMMdd");
            var propsNumNow = (_context.Proposals.Where(e=>e.Pdate.Date==proposal.Pdate.Date)
                                                    .Count()+1).ToString("D3");
            

            proposal.ProposalId = $"P{dateNow}{propsNumNow}";
            proposal.Underways = true;
            proposal.UserId = User.Identity.Name;

            if(ModelState.IsValid)
            {
                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(proposal);
        }
    }
}
