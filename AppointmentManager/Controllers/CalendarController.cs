using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppointmentManager.Controllers
{
    [Authorize]
    [UserAuthorization(AuthOptions.Permissions.ViewCalendarPage)]
    
    public class CalendarController :Controller
    {
        private dbContext db = new dbContext();

        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);
            scheduler.Skin = DHXScheduler.Skins.Flat;

            scheduler.Config.first_hour = 8;
            scheduler.Config.last_hour = 20;

            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            var customers = Service_Customers.GetCustomers();
            var select_customers = new LightboxSelect("Id_Customer", "Customer: ");
            var list_customers = new List<object>();

            foreach(var customer in customers)
            {
                list_customers.Add(new { key = customer.Id, label = customer.Name });
            }

            select_customers.AddOptions(list_customers);
            scheduler.Lightbox.Add(select_customers);

            var textbox_details = new LightboxText("text", "Details");

            scheduler.Lightbox.Add(textbox_details);

            return View(scheduler);
        }
        public ContentResult Data(DateTime from, DateTime to)
        {
            var id_currentUser = User.GetId();

            var apps = db.CalendarAppointments.Where(p=>p.Id_User==id_currentUser).Where(e => e.StartDate < to && e.EndDate >= from).ToList();

            var scheduler = new SchedulerAjaxData(apps);

            scheduler.DateFormat = "%d/%m/%Y %H:%i";

            return scheduler;
        }


        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = DHXEventsHelper.Bind<CalendarAppointment>(actionValues);

                var id_currentUser = User.GetId();

                changedEvent.Id_User = id_currentUser;

                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        db.CalendarAppointments.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception ex)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }


        public FileContentResult DownloadCSV()
        {
            var model = Service_Exports.Export_Appointments_CSV(User);

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/csv", "Export-Appointments.csv");
        }

        public FileContentResult DownloadJSON()
        {
            var model = Service_Exports.Export_Appointments_JSON(User);

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/json", "Export-Appointments.json");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}