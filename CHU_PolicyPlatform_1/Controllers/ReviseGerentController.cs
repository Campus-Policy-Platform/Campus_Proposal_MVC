using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class ReviseGerentController : Controller
    {
        private readonly ProposeContext _context;

        public ReviseGerentController(ProposeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdG()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdG(User user, string ActionType)
        {
            if (ActionType == "Add")
            {
                var uu = _context.Users.ToList();
                var gg = _context.Gerents.ToList();

                var existingRecord = gg.FirstOrDefault(g => g.GerentId == user.UserId);

                if (existingRecord != null)
                {
                    TempData["ErrorMessage"] = "此學號已有管理員身分";
                    return RedirectToAction("AdG", "ReviseGerent");
                }
                else
                {
                    var newg = uu.Where(u => u.UserId == user.UserId).Select(u => new Gerent
                    {
                        GerentId = u.UserId,
                        Gpassword = u.Upassword
                    }).FirstOrDefault();

                    if (ModelState.IsValid)
                    {
                        if (newg != null)
                        {
                            _context.Gerents.Add(newg);
                            _context.SaveChanges();

                            TempData["SuccessMessage"] = "新增成功";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "找不到此學號，請確認後再輸入";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "發生錯誤，請檢查您的輸入並重試";
                    }
                }


            }
            else if (ActionType == "Delete")
            {
                var gg = _context.Gerents.ToList();
                var newg = gg.Where(g => g.GerentId == user.UserId).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    if (newg != null)
                    {
                        if (newg.GerentId == "B001")
                        {
                            TempData["ErrorMessage"] = "此用戶無法刪除";
                        }
                        else
                        {
                            _context.Gerents.Remove(newg);
                            _context.SaveChanges();
                            TempData["SuccessMessage"] = "刪除成功";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "找不到此學號，請確認後再輸入"; 
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "發生錯誤，請檢查您的輸入並重試";
                }
            }

            return RedirectToAction("AdG", "ReviseGerent");
        }
    }
}
