using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Id_User { get; set; }

        public int Id_Customer { get; set; }
        public DateTime Start_datetime { get; set; }

        public DateTime End_datetime { get; set; }

        public string Details { get; set; }

        public bool isDeleted { get; set; }
        
        public virtual AspNetUser User { get; set; }

        public virtual Customer Customer { get; set; }
    }
}