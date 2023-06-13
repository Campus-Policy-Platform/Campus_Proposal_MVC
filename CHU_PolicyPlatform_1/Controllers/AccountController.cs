using Microsoft.AspNetCore.Mvc;
using CHU_PolicyPlatform_1.ViewModels;
using Microsoft.EntityFrameworkCore;
using CHU_PolicyPlatform_1.Models;
using System.Security.Claims;
using CHU_PolicyPlatform_1.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace CHU_PolicyPlatform_1.Controllers
{
    public class AccountController : Controller
    {

        private readonly ProposeContext _ctx;
        public  AccountController(ProposeContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(LoginViewModel loginVM, string action)
        //{

        //    var user = _ctx.Users.Where(u => u.UserId == loginVM.UserId && u.Upassword == loginVM.Password).Count();
        //    var mana = _ctx.Gerents.Where(x => x.GerentId == loginVM.UserId && x.Gpassword == loginVM.Password).Count();
        //    if (action == "使用者")
        //    {
        //        if (user > 0)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");
        //            return View(loginVM);
        //        }
        //    }
        //    else if (action == "管理者")
        //    {
        //        if (mana > 0)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");
        //            return View(loginVM);
        //        }
        //    }
        //    return View(loginVM);
        //}
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginVM) 
        {
            if (ModelState.IsValid) 
            {
                var user = AuthenticateUser(loginVM);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼有錯!!!");

                    return View(loginVM);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,"User")
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

                return RedirectToAction("SalesReports", "Test");
            }
            return View(loginVM);
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }


        private ApplicationUser AuthenticateUser(LoginViewModel loginVM)
        {
            var user = _ctx.Users
                .Where(c => c.UserId == loginVM.UserId && c.Upassword == loginVM.Password).FirstOrDefault();
            if (user != null)
            {
                var userInfo = new ApplicationUser
                {
                    Name = user.UserId
                };
                return userInfo;
            }
            else
            {
                return null;
            }
        }



    }
}
