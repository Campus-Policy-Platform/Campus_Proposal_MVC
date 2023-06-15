using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[Authorize]
        [Authorize(Roles = "User")]
        public IActionResult SalesReports()
        {
            return View();
        }

        [Authorize(Roles ="Gerent")]
        public IActionResult GerentReports() 
        {
            return View();
        }
    }
}
