using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public class DateTimeConverter_ISO : IsoDateTimeConverter
    {
        public DateTimeConverter_ISO()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm";
        }
    }
}