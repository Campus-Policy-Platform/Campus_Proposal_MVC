using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> Advance()
        {
            var cates = await _context.Categories.ToListAsync();
            List<SelectListItem> cateListItem = cates.Select(e =>
                new SelectListItem { Text=$"{e.CategoryId.Substring(2)}.{e.CategoryName}", Value=e.CategoryId}).ToList();

            return View(cateListItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Advance(Proposal proposal)
        {
            
            proposal.Pdate = DateTime.Now;
            var dateNow = proposal.Pdate.ToString("yyMMdd");
            var propsNumNow = (_context.Proposals.Where(e=>e.Pdate.Date==proposal.Pdate.Date).Count()+1).ToString("D3");
            

            proposal.ProposalId = $"P{dateNow}{propsNumNow}";
            proposal.Underways = true;
            proposal.UserId = "B10800123";

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
