using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class MineController : Controller
    {
        private readonly ProposeContext _ctx;
        public MineController(ProposeContext ctx) 
        { 
            _ctx = ctx;
        }
        [Authorize(Roles = "User")]
        public IActionResult Participated()
        {
            List<Proposal> myprops = _ctx.Proposals.Where(e=>e.UserId==User.Identity.Name)
                                                .OrderByDescending(e=>e.Pdate).ToList();
            List<Proposal> votedprops = (from p in _ctx.Proposals
                                        join v in _ctx.Votes on p.ProposalId equals v.ProposalId
                                        where v.UserId == User.Identity.Name
                                        orderby p.Pdate descending
                                        select p).ToList();
            List<Category> categories = new List<Category>(_ctx.Categories);
            JoinedViewModel joinedVM = new JoinedViewModel
            {
                MyProps = myprops,
                VotedProps = votedprops,
                Categories = categories
            };

            return View(joinedVM);
        }
    }
}
