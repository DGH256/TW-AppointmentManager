using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public class UserRole :IComparable<UserRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public float AuthorityLevel { get; set; }

        public List<UserPermission> Permissions { get; set; } = new List<UserPermission>();


        public UserRole(string name, float authorityLevel)
        {
            this.Name = name;
            this.AuthorityLevel = authorityLevel;

            this.Id = HelperClass.GenerateRandomString(UserAuthorizationManager.defaultIdLength);

            UserAuthorizationManager.AddRole(this);
        }

        public UserRole(string name, float authorityLevel, IEnumerable<UserPermission> permissions) : this(name,authorityLevel)
        {
            if(permissions!= null)
            {
                foreach(var permission in permissions)
                {
                    this.Permissions.Add(permission);
                }
            }
        }

        public UserRole(string name, float authorityLevel, IEnumerable<String> permissions) : this(name, authorityLevel)
        {
            if (permissions != null)
            {
                foreach (var permissionName in permissions)
                {
                    var permission = UserAuthorizationManager.getPermission(permissionName);

                    this.Permissions.Add(permission);
                }
            }
        }

        public int CompareTo(UserRole obj)
        {
            return (this.AuthorityLevel <= obj.AuthorityLevel)  ? 1 : -1;
        }
    }
}