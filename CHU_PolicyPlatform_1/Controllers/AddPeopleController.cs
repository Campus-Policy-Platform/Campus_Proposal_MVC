using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class AddPeopleController : Controller
    {

        private readonly ProposeContext _context;

        public AddPeopleController(ProposeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdU()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult AdU(IFormFile csvFile)
        {

            if (csvFile == null || csvFile.Length == 0)
            {
                TempData["ErrorMessage"] = "你未選擇檔案。";
                return RedirectToAction("AdU", "AddPeople");
            }

            string fileExtension = Path.GetExtension(csvFile.FileName);
            if (fileExtension != ".csv")
            {
                TempData["ErrorMessage"] = "只接受 CSV 檔案。";
                return RedirectToAction("AdU", "AddPeople");
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
            return RedirectToAction("AdU", "AddPeople");
        }

    }
}
