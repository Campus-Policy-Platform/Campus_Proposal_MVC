using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class GerentReviewDayController : Controller
    {
        private readonly ProposeContext _context;

        public GerentReviewDayController(ProposeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Gerent")]
        [HttpGet]
        public IActionResult RDC()
        {
            var categories = _context.Categories.ToList();
            var value = new RdVM
            {
                Categories = categories,
            };

            return View(value);
        }
        [HttpPost("/GerentReviewDay/UploadVM")]
        public IActionResult UploadVM( string categoryId)
        {
            var categoryToUpdate = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            var updatedCategories = _context.Categories.ToList();
            var updatedValue = new RdVM
            {
                CategoryId = categoryId,
                CategoryMinDay = categoryToUpdate.CategoryMinDay,
                CategoryMaxDay = categoryToUpdate.CategoryMaxDay,
                CategoryGerentReview = categoryToUpdate.CategoryGerentReview,
                CategoryName = categoryToUpdate.CategoryName,
                Categories = updatedCategories
            };

            return View("RDC", updatedValue);
        }
        [HttpPost("/GerentReviewDay/SaveRD")]
        public IActionResult SaveRD(Category category ,string ActionType ,string categoryId) 
        {
            var categoryToUpdate = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);


            if (categoryToUpdate != null)
            {
                if (ActionType == "remin")
                {
                    categoryToUpdate.CategoryMinDay = category.CategoryMinDay;
                    TempData["SuccessMessage"] = $"修改成功，現在《{categoryToUpdate.CategoryName}》案件時程最小天數為：{categoryToUpdate.CategoryMinDay}天";
                }
                else if (ActionType == "remax")
                {
                    categoryToUpdate.CategoryMaxDay = category.CategoryMaxDay;
                    TempData["SuccessMessage"] = $"修改成功，現在《{categoryToUpdate.CategoryName}》案件時程最大天數為：{categoryToUpdate.CategoryMaxDay}天";
                }
                else if (ActionType == "cgr")
                {
                    categoryToUpdate.CategoryGerentReview = category.CategoryGerentReview;
                    TempData["SuccessMessage"] = $"修改成功，現在《{categoryToUpdate.CategoryName}》票數門檻為：{categoryToUpdate.CategoryGerentReview}票";
                }

                _context.SaveChanges();
            }
            return RedirectToAction("RDC", "GerentReviewDay");

        }

    }
}
