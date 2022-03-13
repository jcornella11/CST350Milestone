using Microsoft.AspNetCore.Mvc;
using CST350Milestone.Models;
using CST350Milestone.Services;

namespace CST350Milestone.Controllers
{
    public class RegistrationController : Controller
    {
        SecurityService securityService = new SecurityService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel user)
        {
            // If insert is successful, go to details page
            if(securityService.registration(user))
                return View(user);

            // Else return to index
            return View("Index", user);
        }
    }
}
