using AppointmentManager.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager.Models
{
    public class vm_UserInfo
    {

        public string Id { get; set; }

        [EditableProperty]
        public string Email { get; set; }

        [EditableProperty]
        public string Nickname { get; set; }

        [EditableProperty]
        public string Address { get; set; }

        [EditableProperty]
        public string Details { get; set; }

        [EditableProperty]
        public string CNP { get; set; }

        List<UserRole> Roles = new List<UserRole>();
    }
}