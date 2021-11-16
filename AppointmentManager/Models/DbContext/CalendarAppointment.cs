using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DHTMLX.Scheduler;

namespace AppointmentManager
{
    public class CalendarAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        public string Description { get; set; }

        [DHXJson(Alias = "start_date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DHXJson(Alias = "end_date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        [DHXJson(Alias = "Id_User")]
        public string Id_User { get; set; }

        [DHXJson(Alias = "Id_Customer")]
        public int Id_Customer { get; set; }
    }
}