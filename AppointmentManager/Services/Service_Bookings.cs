using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public static class Service_Bookings
    {
        public static List<Booking_Room> GetRooms()
        {
            using (dbContext context = new dbContext())
            {
                return context.Booking_Rooms.Where(p => p.isDeleted == false).OrderBy(p=>p.title).ToList();
            }
        }

        public static void AddRoom(Booking_Room booking)
        {
            using (dbContext context = new dbContext())
            {
                context.Booking_Rooms.Add(booking);

                context.SaveChanges();
            }
        }

        public static void UpdateRoom(Booking_Room booking)
        {
            using (dbContext context = new dbContext())
            {
                var oldBooking = context.Booking_Rooms.Where(p => p.id == booking.id).FirstOrDefault();

                oldBooking.CopyProperties(booking);

                context.SaveChanges();
            }
        }

        public static void DeleteRoom(int id)
        {
            using (dbContext context = new dbContext())
            {
                var booking = context.Booking_Rooms.Where(p => p.id == id).FirstOrDefault();

                booking.isDeleted = true;

                context.SaveChanges();
            }
        }

        public static List<Booking_Event> GetEvents()
        {
            using (dbContext context = new dbContext())
            {
                return context.Booking_Events.Where(p => p.isDeleted == false).OrderBy(p=>p.start).ToList();
            }
        }

        public static void AddEvent(Booking_Event bookingEvent)
        {
            using (dbContext context = new dbContext())
            {
                context.Booking_Events.Add(bookingEvent);

                context.SaveChanges();
            }
        }

        public static void UpdateEvent(Booking_Event bookingEvent)
        {
            using (dbContext context = new dbContext())
            {
                var oldEvent = context.Booking_Events.Where(p => p.id == bookingEvent.id).FirstOrDefault();

                oldEvent.CopyProperties(bookingEvent);

                context.SaveChanges();
            }
        }

        public static void DeleteEvent(int id)
        {
            using (dbContext context = new dbContext())
            {
                var bookingEvent = context.Booking_Events.Where(p => p.id == id).FirstOrDefault();

                bookingEvent.isDeleted = true;

                context.SaveChanges();
            }
        }
    }
}