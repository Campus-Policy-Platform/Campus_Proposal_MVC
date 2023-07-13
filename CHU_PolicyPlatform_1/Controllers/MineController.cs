using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Services;
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
        private readonly SeGerent _seGerent;
        public MineController(ProposeContext ctx, SeGerent seGerent) 
        { 
            _ctx = ctx;
            _seGerent = seGerent;
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

            var Cateprop = _seGerent.SeCates();
            var cates = new List<cateM>();
            foreach (var cate in Cateprop)
            {
                cates.Add(new cateM
                {
                    CategoryId = cate.CategoryId,
                    CategoryName = cate.CategoryName,
                });
            };
            var geseVM = new GerentSeeVM();
            var pies = new List<propM>();
            foreach (var prop in myprops)
            {
                pies.Add(new propM
                {
                    ProposalId = prop.ProposalId,
                    Title = prop.Title,
                    CategoryId = prop.CategoryId,
                });
            };            
            geseVM = new GerentSeeVM
            {
                pieVMs = pies,
                categories = cates
            };

            var vgeseVM = new GerentSeeVM();
            pies.Clear();
            foreach (var prop in myprops)
            {
                pies.Add(new propM
                {
                    ProposalId = prop.ProposalId,
                    Title = prop.Title,
                    CategoryId = prop.CategoryId,
                });
            };
            vgeseVM = new GerentSeeVM
            {
                pieVMs = pies,
            };

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
