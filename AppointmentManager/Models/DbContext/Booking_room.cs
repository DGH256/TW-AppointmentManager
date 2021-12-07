using Newtonsoft.Json;
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
    public class Booking_Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [EditableProperty]
        public string title { get; set; }
        [EditableProperty]
        public string eventColor { get; set; }

        [ScriptIgnore]
        [JsonIgnore]
        [EditableProperty]
        public bool isDeleted { get; set; }

        [JsonIgnore]
        [ScriptIgnore]
        public virtual ICollection<Booking_Event> Booking_Events { get; set; }
    }
}