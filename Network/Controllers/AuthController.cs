using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Network.App_Start;
using Network.Models;
using Network.Models.Auth;
using Network.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class AuthController : Controller
    {
        ApplicationDbContext context;
        private ApplicationDbContext Context
        {
            get
            {
                if (context == null) context = new ApplicationDbContext();
                return context;
            }
        }

        UserManager mgr;
        private UserManager UserManager
        {
            get
            {
                if (mgr == null) mgr = new UserManager(new UserStore<ApplicationUser>(Context));
                return mgr;
            }
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LoginViewModel details, string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            UserStore Store = new UserStore(new ApplicationDbContext());
            UserManager userManager = new UserManager(Store);
            ApplicationUser user = await userManager.FindAsync(details.EmailOrUsername, details.Password);
            if (user == null)
            {
                user = await userManager.FindByEmailAsync(details.EmailOrUsername);
                if (user != null)
                    user = await userManager.CheckPasswordAsync(user, details.Password) ? user : null;
            }

            if (user == null)
                ModelState.AddModelError("", "Неверно введено имя пользователя или пароль.");
            else
            {
                ClaimsIdentity ident = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                return Redirect(returnUrl);
            }

            return View(details);
        }

        public ActionResult LogOut(string returnUrl)
        {
            AuthManager.SignOut();
            return Redirect(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home", null);
                else
                    AddErrorsFromResult(result);
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
                ModelState.AddModelError("", error);
        }
    }
}