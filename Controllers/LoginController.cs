using Microsoft.AspNetCore.Mvc;
using CST350Milestone.Models;
using CST350Milestone.Services;

namespace CST350Milestone.Controllers
{
    public class LoginController : Controller
    {
        SecurityService securityService = new SecurityService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(UserModel user)
        {
            if (securityService.login(user))
            {
                user = securityService.setUserProperties(user);
                return View("LoginSuccess", user);
            }

            else
            {
                return View("Index", user);
            }
        }
    }
}
