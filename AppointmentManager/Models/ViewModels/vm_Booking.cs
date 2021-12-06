using AppointmentManager.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager.Models
{
    public class vm_Booking
    {
        public IEnumerable<Booking_Event> BookingEvents { get; set; }

        public IEnumerable<Booking_Room> BookingRooms { get; set; }
    }
}