using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class VoteController : Controller
    {
        private readonly ProposeContext _context;
        public VoteController(ProposeContext context) 
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult ProposeVote(string propId, string vote="1")
        {
            string userId = User.Identity.Name;
            propId = "P230612001";

            if (_context.Votes.ToList().Find(e => e.UserId == userId && e.ProposalId==propId) != null)
            {
                return NotFound();
            }

            var proposal = _context.Proposals.ToList().Find(e=>e.ProposalId==propId);

            bool crucial;
            switch (vote)
            {
                case "1":
                    crucial = true;
                    break;
                case "0":
                    crucial = false;
                    break;
                default:
                    return NotFound();
            }

            ViewData["UserId"] = userId;
            ViewData["Crucial"] = crucial;

            return View(proposal);
        }

        [HttpPost]
        public async Task<IActionResult> ProposeVote(Vote vote)
        {
            vote.Vdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Votes.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(vote);
        }
    }
}
