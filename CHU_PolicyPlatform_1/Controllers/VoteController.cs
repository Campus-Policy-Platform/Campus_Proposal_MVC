using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public IActionResult ProposeVote(string propId, string vote)
        {
            string userId = User.Identity.Name;

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
            PollViewModel pollVM = new PollViewModel
            {
                proposal = proposal,
                vote = new Vote { Crucial = crucial, ProposalId = proposal.ProposalId, UserId = userId }
            };

            return View(pollVM);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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
