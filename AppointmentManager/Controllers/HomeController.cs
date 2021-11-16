using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult PersonalSettings()
        {
            var settings = Service_Users.GetUserSettings(User);

            return View(settings);
        }


        public ActionResult AccessDenied()
        {
            ViewBag.Message = "Unauthorized to view this page";

            return View("Error_InsufficientPermission");
        }



    }
}