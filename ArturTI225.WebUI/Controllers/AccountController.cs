using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ArturTI225.BusinessLogic.Helpers;
using ArturTI225.BusinessLogic.Managers;
using ArturTI225.Domain.Entities.Auth;
using ArturTI225.Infrastructure;

namespace ArturTI225.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private AuthenticationManager authManager = new AuthenticationManager();
        private CarsShopContext db = new CarsShopContext();

        public ActionResult Login()
        {
            // Если пользователь уже аутентифицирован, редиректим на главную страницу
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = authManager.Authenticate(username, password);
            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(
                    1, // version
                    user.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20), // expire
                    true, // persistent
                    user.Role, // user roles
                    "/");

                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            // Регистрация пользователя и добавление роли по умолчанию (если это необходимо)
            var user = authManager.Register(username, password);
            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(
                    1, // version
                    user.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20), // expire
                    true, // persistent
                    user.Role, // user roles
                    "/");

                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            // Удаление аутентификационной куки
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            
            // Редирект на страницу входа
            return RedirectToAction("Login", "Account");
        }
    }
}
