using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class ReviseMemberController : Controller
    {
        private readonly ProposeContext _context;

        public ReviseMemberController(ProposeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Revise()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Revise(User user, string ActionType)
        {
            if (ActionType == "Add")
            {
                var uu = _context.Users.ToList();
                var gg = _context.Gerents.ToList();

                var existingRecord = gg.FirstOrDefault(g => g.GerentId == user.UserId);

                if (existingRecord != null)
                {
                    TempData["ErrorMessagess"] = "此學號已有管理員身分";
                    return RedirectToAction("Revise", "ReviseMember");
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

                            TempData["SuccessMessagess"] = "新增成功";
                        }
                        else
                        {
                            TempData["ErrorMessagess"] = "找不到此學號，請確認後再輸入";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessagess"] = "發生錯誤，請檢查您的輸入並重試";
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
                            TempData["ErrorMessagess"] = "此用戶無法刪除";
                        }
                        else
                        {
                            _context.Gerents.Remove(newg);
                            _context.SaveChanges();
                            TempData["SuccessMessagess"] = "刪除成功";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessagess"] = "找不到此學號，請確認後再輸入";
                    }
                }
                else
                {
                    TempData["ErrorMessagess"] = "發生錯誤，請檢查您的輸入並重試";
                }
            }

            return RedirectToAction("Revise", "ReviseMember");
        }
    
        [HttpPost("/ReviseMember/UploadUsers")]
        public IActionResult UploadUsers(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                TempData["ErrorMessage"] = "你未選擇檔案。";
                return RedirectToAction("Revise", "ReviseMember");
            }

            string fileExtension = Path.GetExtension(csvFile.FileName);
            if (fileExtension != ".csv")
            {
                TempData["ErrorMessage"] = "只接受 CSV 檔案。";
                return RedirectToAction("Revise", "ReviseMember");
            }


            using (StreamReader reader = new StreamReader(csvFile.OpenReadStream()))
            {
                reader.ReadLine();

                List<string> errorMessages = new List<string>();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    string userId = fields[0];
                    string upassword = fields[1];


                    if (_context.Users.Any(u => u.UserId == userId))
                    {
                        errorMessages.Add($"使用者 {userId} 已存在，載入已跳過。");
                        continue;
                    }


                    User user = new User
                    {
                        UserId = userId,
                        Upassword = upassword
                    };

                    _context.Users.Add(user);
                }

                _context.SaveChanges();

                if (errorMessages.Count > 0)
                {
                    TempData["ErrorMessages"] = errorMessages;
                }
            }

            TempData["SuccessMessage"] = "新增成功";
            return RedirectToAction("Revise", "ReviseMember");
        }
    }
}
