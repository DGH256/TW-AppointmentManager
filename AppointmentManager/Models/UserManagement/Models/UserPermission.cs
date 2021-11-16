using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public class UserPermission
    { 
        public string Id { get; set; }

        public string Name { get; set; }

        public UserPermission(string id, string name)
        {
            this.Id = id;
            this.Name = name;

            if(this.Id== null)
            {
                this.Id = HelperClass.GenerateRandomString(UserAuthorizationManager.defaultIdLength);
            }

            UserAuthorizationManager.AddPermission(this);
        }

        public UserPermission(string name)
        {
            this.Name = name;
            this.Id = HelperClass.GenerateRandomString(UserAuthorizationManager.defaultIdLength);
            UserAuthorizationManager.AddPermission(this);
        }

    }
}