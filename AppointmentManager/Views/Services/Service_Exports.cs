using AppointmentManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace AppointmentManager
{
    public static class Service_Exports
    {

        public static string Export_Customers_CSV()
        {
            var customers = Service_Customers.GetCustomers();

            var content = customers.ExportCsv().ToString();

            return content;

        }

        public static string Export_Customers_JSON()
        {
            var customers = Service_Customers.GetCustomers();

            var content = customers.JsonSerialize();

            return content;
        }

        public static string Export_Users_CSV()
        {
            var users = Service_Users.GetAllUsers();

            var content = users.ExportCsv().ToString();

            return content;
        }

        public static string Export_Users_JSON()
        {
            var users = Service_Users.GetAllUsers();

            var content = users.JsonSerialize();

            return content;
        }

        public static string Export_Appointments_CSV(IPrincipal user)
        {
            var userId = user.GetId();

            var appointments = Service_Appointments.GetAppointments(userId);

            var content = appointments.ExportCsv().ToString();

            return content;
        }

        public static string Export_Appointments_JSON(IPrincipal user)
        {
            var userId = user.GetId();

            var appointments = Service_Appointments.GetAppointments(userId);

            var content = appointments.JsonSerialize();

            return content;
        }

        public static string Export_Rooms_CSV()
        {
            var items = Service_Bookings.GetRooms();

            var content = items.ExportCsv().ToString();

            return content;
        }

        public static string Export_Rooms_JSON()
        {
            var items = Service_Bookings.GetRooms();

            var content = items.JsonSerialize();

            return content;
        }

        public static string Export_Events_CSV()
        {
            var items = Service_Bookings.GetEvents();

            var content = items.ExportCsv().ToString();

            return content;
        }

        public static string Export_Events_JSON()
        {
            var items = Service_Bookings.GetEvents();

            var content = items.JsonSerialize();

            return content;
        }

        private static StringBuilder ExportCsv<T>(this List<T> genericList, string separator="; ")
        {
            var sb = new StringBuilder();
            var header = "";
            var properties = typeof(T).GetProperties().Where(p => p.GetGetMethod().IsVirtual == false); // getting all properties except the virtual ones
            properties = properties.Where(p => Attribute.IsDefined(p, typeof(JsonIgnoreAttribute)) == false).Where(p => Attribute.IsDefined(p, typeof(XmlIgnoreAttribute)) == false);


            foreach (var prop in properties)
           { 
                header += prop.Name + separator;
            }

            header = header.Substring(0, header.Length - 2);
            sb.AppendLine(header);

            foreach (var obj in genericList)
            {
                var line = "";
                foreach (var prop in properties)
                {
                    line += prop.GetValue(obj, null) + separator;
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
            }

            return sb;
        }

        private static T XmlDeserialize<T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        private static string XmlSerialize<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        private static T JsonDeserialize<T>(this string toDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(toDeserialize);
        }

        private static string JsonSerialize<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}