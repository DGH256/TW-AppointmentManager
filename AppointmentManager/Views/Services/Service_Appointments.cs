using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public static class Service_Appointments
    {
        public static List<CalendarAppointment> GetAppointments(string userId)
        {
            using (dbContext context = new dbContext())
            {
                return context.CalendarAppointments.Where(p => p.Id_User == userId).ToList();
            }
        }
    }
}