using AppointmentManager.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AppointmentManager
{

    public static partial class Extensions
    {
        public static bool HasPermission(this IPrincipal user, UserPermission permission)
        {
            return UserAuthorizationManager.UserHasPermission(user, permission);
        }

        public static bool HasPermission(this IPrincipal user, string permission)
        {
            return UserAuthorizationManager.UserHasPermission(user, permission);
        }

        public static bool HasRole(this IPrincipal user, string role)
        {
            return UserAuthorizationManager.UserHasRole(user, role);
        }

        public static bool HasRole(this IPrincipal user, UserRole role)
        {
            return UserAuthorizationManager.UserHasRole(user, role);
        }

        public static AspNetUser GetUserSettings(this IPrincipal user)
        {
            return Service_Users.GetUserSettings(user);
        }


        public static string GetId(this IPrincipal user)
        {
            return UserAuthorizationManager.GetUserId(user);
        }

        public static bool HasAuthority(this IPrincipal user, UserRole role)
        {
            return UserAuthorizationManager.UserHasAuthority(user, role);
        }
    }
}