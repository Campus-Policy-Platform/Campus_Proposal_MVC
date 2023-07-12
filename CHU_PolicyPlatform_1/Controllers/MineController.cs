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
            List<Proposal> propsunde = myprops.Where(e => e.Underways == true).ToList();
            List<Proposal> propspass = myprops.Where(e => e.Underways == false).ToList();
            List<Proposal> votedprops = (from p in _ctx.Proposals
                                        join v in _ctx.Votes on p.ProposalId equals v.ProposalId
                                        where v.UserId == User.Identity.Name
                                        orderby p.Pdate descending
                                        select p).ToList();
            List<Proposal> voteunde = votedprops.Where(e => e.Underways == true).ToList();
            List<Proposal> votepass = votedprops.Where(e => e.Underways == false).ToList();
            List<Category> categories = new List<Category>(_ctx.Categories);

            List<GerentSeeVM> geseVM = new List<GerentSeeVM>();
            foreach (var prop in myprops)
            {
                geseVM.Add(new GerentSeeVM
                {
                    ProposalId = prop.ProposalId,
                    Title = prop.Title,
                    CategoryId = prop.CategoryId
                });
            }
            List<GerentSeeVM> vgeseVM = new List<GerentSeeVM>();
            foreach (var prop in votedprops)
            {
                vgeseVM.Add(new GerentSeeVM
                {
                    ProposalId = prop.ProposalId,
                    Title = prop.Title,
                    CategoryId = prop.CategoryId
                });
            }

            JoinedViewModel joinedVM = new JoinedViewModel
            {
                GeseVM = geseVM,
                Propsunde = propsunde,
                Propspass = propspass,
                VgeseVM = vgeseVM,
                Voteunde = voteunde,
                Votepass = votepass,
                Categories = categories
            };

            return View(joinedVM);
        }
    }
}
