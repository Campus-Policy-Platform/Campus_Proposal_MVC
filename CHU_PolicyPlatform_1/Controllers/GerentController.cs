using Microsoft.AspNetCore.Mvc;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.EntityFrameworkCore;
using CHU_PolicyPlatform_1.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Controllers
{
    public class GerentController : Controller
    {
        private readonly ProposeContext _ctx;

        public GerentController(ProposeContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GerentLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GerentLoginAsync(LoginViewModel GerentLoginVM)
        {

            if (ModelState.IsValid)
            {
                var gerent = AuthenticateUser(GerentLoginVM);

                if (gerent == null)
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");

                    return View(GerentLoginVM);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,gerent.GerentName),
                    new Claim(ClaimTypes.Role,"Gerent")
                    //new Claim(ClaimTypes.Role, user.Role) // 如果要有「群組、角色、權限」，可以加入這一段  
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return RedirectToAction("GerentReports", "Test");
            }
            return View(GerentLoginVM);
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }


        private ApplicationUser AuthenticateUser(LoginViewModel loginVM)
        {
            var Genert =_ctx.Gerents.
                Where(g => g.GerentId == loginVM.Id && g.Gpassword == loginVM.Password).FirstOrDefault();
            if (Genert != null)
            {
                var gerentInfo = new ApplicationUser
                {
                    GerentName = Genert.GerentId
                    //Role = "Gerent"
                };
                return gerentInfo;
            }
            else
            {
                return null;
            }
        }
    }

}
