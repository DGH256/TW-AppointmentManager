using AppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace AppointmentManager.Controllers
{
    [Authorize]
    [UserAuthorization(AuthOptions.Permissions.ViewCustomersPage)]
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            var model = Service_Customers.GetCustomers();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public void UpdateCustomer(Customer customer)
        {
            Service_Customers.UpdateCustomer(customer);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCustomer(Customer customer)
        {
            Service_Customers.AddCustomer(customer);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteCustomer(int id)
        {
            Service_Customers.DeleteCustomer(id);

            return this.RedirectToAction("Index");
        }

        public FileContentResult DownloadCSV()
        {
            var model = Service_Exports.Export_Customers_CSV();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/csv", "Export-Customers.csv");
        }

        public FileContentResult DownloadJSON()
        {
            var model = Service_Exports.Export_Customers_JSON();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/json", "Export-Customers.json");
        }

    }
}