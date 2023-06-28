using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

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
            if (ModelState.IsValid)
            {
                _context.ToReponds.Add(toRepond);
                _context.SaveChangesAsync();
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("GerentSee", "Gerentcase");
            }
            return View(toRepond);
        }
    }
}
