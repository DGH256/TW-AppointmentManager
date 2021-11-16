using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager.UserManagement
{
    public class UserCacheItem
    {
        public HashSet<string> roles { get; set; } = null;

        public HashSet<string> permissions { get; set; } = null;

        public string Id { get; set; } = null;
    }
}