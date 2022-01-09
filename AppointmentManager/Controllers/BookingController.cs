using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentManager.Models;

namespace AppointmentManager.Controllers
{
    [Authorize]
    [UserAuthorization(AuthOptions.Permissions.ViewCalendarPage)]
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            var rooms = Service_Bookings.GetRooms();
            var events = Service_Bookings.GetEvents();
            var customers = Service_Customers.GetCustomers();

 
            vm_Booking model = new vm_Booking();
            model.BookingEvents = events;
            model.BookingRooms = rooms;
            model.Customers = customers;

            return View(model);
        }

        public ActionResult Rooms()
        {
            var model = Service_Bookings.GetRooms();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public void UpdateBooking(Booking_Room booking)
        {
            Service_Bookings.UpdateRoom(booking);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateEvent(Booking_Event bookingEvent)
        {
            Service_Bookings.UpdateEvent(bookingEvent);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddBooking(Booking_Room booking)
        {
            Service_Bookings.AddRoom(booking);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteBooking(int id)
        {
            Service_Bookings.DeleteRoom(id);

            return this.RedirectToAction("Index");
        }

        public ActionResult Events()
        {
            var rooms = Service_Bookings.GetRooms();
            var events = Service_Bookings.GetEvents();


            vm_Booking model = new vm_Booking();
            model.BookingEvents = events;
            model.BookingRooms = rooms;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddEvent(Booking_Event bookingEvent)
        {
            Service_Bookings.AddEvent(bookingEvent);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteEvent(int id)
        {
            Service_Bookings.DeleteEvent(id);

            return this.RedirectToAction("Index");
        }

        [Authorize]
        public FileContentResult DownloadRoomsCSV()
        {
            var model = Service_Exports.Export_Rooms_CSV();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/csv", "Export-Rooms.csv");
        }
        [Authorize]
        public FileContentResult DownloadRoomsJSON()
        {
            var model = Service_Exports.Export_Rooms_JSON();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/json", "Export-Rooms.json");
        }

        [Authorize]
        public FileContentResult DownloadEventsCSV()
        {
            var model = Service_Exports.Export_Events_CSV();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/csv", "Export-Events.csv");
        }

        [Authorize]
        public FileContentResult DownloadEventsJSON()
        {
            var model = Service_Exports.Export_Events_JSON();

            return File(new System.Text.UTF8Encoding().GetBytes(model), "text/json", "Export-Events.json");
        }
    }
}