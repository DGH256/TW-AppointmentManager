using AppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentManager.Controllers
{
    [Authorize]
    [UserAuthorization(AuthOptions.Permissions.ViewUsersPage)]
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            var model = Service_Users.GetAllUsers();

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePersonalSettings(AspNetUser settings)
        {     
            Service_Users.SetPersonalSettings(User, settings);

            return RedirectToAction("PersonalSettings","Home");
        }

        [HttpPost]
        [Authorize]
        //[UserAuthorization(AuthOptions.Roles.SuperAdmin)]
        public void UpdateUserSettings(AspNetUser newSettings)
        {
            var roles = Request.Params["roles"];

            Service_Users.UpdateUserSettings(newSettings,roles);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePasswordAction(ResetPasswordViewModel model)
        {
            if (!String.IsNullOrEmpty(model.Password))
                if (model.Password == model.ConfirmPassword)
                {
                    var Id = UserAuthorizationManager.GetUserId(User);

                    return RedirectToAction("ChangePassword", "Account", new { userId = Id, newPassword = model.Password });
                }
            return RedirectToAction("PersonalSettings");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteUser(string id)
        {
            Service_Users.DeleteUser(id);

            return this.RedirectToAction("Index");
        }

        public FileContentResult DownloadCSV()
        {
            var model = Service_Exports.Export_Users_CSV();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/csv", "Export-Users.csv");
        }

        public FileContentResult DownloadJSON()
        {
            var model = Service_Exports.Export_Users_JSON();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/json", "Export-Users.json");
        }

    }
}