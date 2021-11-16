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
    public class Customer
    {
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [EditableProperty]
        public string Email { get; set; }
        [EditableProperty]
        public string GovernmentID { get; set; }
        [EditableProperty]
        public string Details { get; set; }
        [EditableProperty]
        public string PhoneNumber { get; set; }
        [EditableProperty]
        public string Address { get; set; }
        [EditableProperty]
        public string Name { get; set; }
        [EditableProperty]
        public bool isDeleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}