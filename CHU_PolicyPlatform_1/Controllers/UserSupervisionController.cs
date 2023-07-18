using CHU_PolicyPlatform_1.Services;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class UserSupervisionController : Controller
    {
        private readonly SeGerent _seGerent;
        private readonly SeUserS _seUserS;

        public UserSupervisionController(SeGerent seGerent, SeUserS seUserS)
        {
            _seGerent = seGerent;
            _seUserS = seUserS;
        }
        public IActionResult Index()
        {
            return View();
        }

        // 計算頁碼的方法
        private int CalculatePages(int totalRows, int pageRows)
        {
            int Pages = totalRows / pageRows;
            if (totalRows % pageRows != 0)
            {
                Pages++;
            }
            return Pages;
        }
        [Authorize(Roles = "User")]
        public IActionResult UserPending(int Id = 1)
        {

            var Passprop = _seGerent.SeUser();
            var Cateprop = _seGerent.SeCates();
            var value = new GerentSeeVM();
            var pies = new List<propM>();
            var cates = new List<cateM>();

            // 計算總筆數和分頁相關參數
            int totalRows = Passprop.Count;
            int activePage = Id;
            int pageRows = 8;

            int Pages = CalculatePages(totalRows, pageRows); // 計算總頁數

            if (Id > Pages)
            {
                return RedirectToAction("ErrorUserPending", "UserSupervision");
            }

            int startRow = (activePage - 1) * pageRows;  // 起始記錄索引
            var pagedPassprop = Passprop.Skip(startRow).Take(pageRows);

            foreach (var Passpop in Passprop)
            {
                pies.Add(new propM
                {
                    ProposalId = Passpop.ProposalId,
                    Title = Passpop.Title,
                    CategoryId = Passpop.CategoryId,
                });
            };
            foreach (var cate in Cateprop)
            {
                cates.Add(new cateM
                {
                    CategoryId = cate.CategoryId,
                    CategoryName = cate.CategoryName,
                });
            };
            value = new GerentSeeVM
            {
                pieVMs = pies,
                categories = cates
            };

            ViewData["ActivePage"] = Id;    // 目前選中的分頁碼
            ViewData["Pages"] = Pages;  // 頁數

            return View(value);
        }
        [Authorize(Roles = "User")]
        public IActionResult ErrorUserPending()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult UserFinished(int Id = 1)
        {
            var Passprop = _seUserS.SeU();
            var Cateprop = _seGerent.SeCates();
            var value = new GerentSeeVM();
            var pies = new List<propM>();
            var cates = new List<cateM>();

            // 計算總筆數和分頁相關參數
            int totalRows = Passprop.Count;
            int activePage = Id;
            int pageRows = 8;

            int Pages = CalculatePages(totalRows, pageRows); // 計算總頁數

            if (Id > Pages)
            {
                return RedirectToAction("ErrorUserFinished", "UserSupervision");
            }

            int startRow = (activePage - 1) * pageRows;  // 起始記錄索引
            var pagedPassprop = Passprop.Skip(startRow).Take(pageRows);

            foreach (var Passpop in Passprop)
            {
                pies.Add(new propM
                {
                    ProposalId = Passpop.ProposalId,
                    Title = Passpop.Title,
                    CategoryId = Passpop.CategoryId,
                });
            };
            foreach (var cate in Cateprop)
            {
                cates.Add(new cateM
                {
                    CategoryId = cate.CategoryId,
                    CategoryName = cate.CategoryName,
                });
            };
            value = new GerentSeeVM
            {
                pieVMs = pies,
                categories = cates
            };

            ViewData["ActivePage"] = Id;    // 目前選中的分頁碼
            ViewData["Pages"] = Pages;  // 頁數

            return View(value);
        }
        [Authorize(Roles = "User")]
        public IActionResult ErrorUserFinished()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult UserFail(int Id = 1)
        {

            var Passprop = _seGerent.SeUserfail();
            var Cateprop = _seGerent.SeCates();
            var value = new GerentSeeVM();
            var pies = new List<propM>();
            var cates = new List<cateM>();

            // 計算總筆數和分頁相關參數
            int totalRows = Passprop.Count;
            int activePage = Id;
            int pageRows = 8;

            int Pages = CalculatePages(totalRows, pageRows); // 計算總頁數

            if (Id > Pages)
            {
                return RedirectToAction("ErrorUserFail", "UserSupervision");
            }

            int startRow = (activePage - 1) * pageRows;  // 起始記錄索引
            var pagedPassprop = Passprop.Skip(startRow).Take(pageRows);

            foreach (var Passpop in Passprop)
            {
                pies.Add(new propM
                {
                    ProposalId = Passpop.ProposalId,
                    Title = Passpop.Title,
                    CategoryId = Passpop.CategoryId,
                });
            };
            foreach (var cate in Cateprop)
            {
                cates.Add(new cateM
                {
                    CategoryId = cate.CategoryId,
                    CategoryName = cate.CategoryName,
                });
            };
            value = new GerentSeeVM
            {
                pieVMs = pies,
                categories = cates
            };

            ViewData["ActivePage"] = Id;    // 目前選中的分頁碼
            ViewData["Pages"] = Pages;  // 頁數

            return View(value);
        }
        [Authorize(Roles = "User")]
        public IActionResult ErrorUserFail()
        {
            return View();
        }
    }
}
