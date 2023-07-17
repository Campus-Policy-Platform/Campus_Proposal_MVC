using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class CategoryAddController : Controller
    {
        private readonly ProposeContext _context;
        public CategoryAddController(ProposeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CyAd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CyAd(Category category)
        {
            // 取得目前資料庫中最新的CategoryId
            string lastCategoryId = await _context.Categories
                .OrderByDescending(c => c.CategoryId)
                .Select(c => c.CategoryId)
                .FirstOrDefaultAsync();

            // 生成新的CategoryId
            string newCategoryId = GenerateNewCategoryId(lastCategoryId);

            // 設定Category的CategoryId
            category.CategoryId = newCategoryId;

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(category);
        }

        // 生成新的CategoryId
        private string GenerateNewCategoryId(string lastCategoryId)
        {
            if (string.IsNullOrEmpty(lastCategoryId))
            {
                // 若資料庫中沒有任何CategoryId，從C001開始
                return "C001";
            }
            else
            {
                // 從最新的CategoryId中取得編號部分的數字，進行加1
                int lastNumber = int.Parse(lastCategoryId.Substring(1));
                int newNumber = lastNumber + 1;

                // 格式化新的編號，補上前面的C並保持編號長度為3
                return $"C{newNumber.ToString("D3")}";
            }
        }


    }
}
