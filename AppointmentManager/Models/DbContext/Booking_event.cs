using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace AppointmentManager
{
    [Serializable]
    public class Booking_Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [EditableProperty]
        public string title { get; set; }

        [EditableProperty]
        public string description { get; set; }

        [EditableProperty]
        public int? customerId { get; set; }

        [EditableProperty]
        public int resourceId { get; set; }

        [EditableProperty]
        [JsonConverter(typeof(DateTimeConverter_ISO))]
        public DateTime start { get; set; }

        [EditableProperty]
        [JsonConverter(typeof(DateTimeConverter_ISO))]
        public DateTime end { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        [EditableProperty]
        public bool isDeleted { get; set; }


        [ScriptIgnore]
        [JsonIgnore]
        public virtual Booking_Room Booking_Resource { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
