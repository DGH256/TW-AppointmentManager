using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppointmentManager.UserManagement;

namespace AppointmentManager
{
    public class UserAuthorizationAttribute : AuthorizeAttribute
    {
        // or inject it
        private DbContext dnContext = new dbContext();

        private UserPermission _permission = null;

        private UserRole _role = null;

        public UserAuthorizationAttribute(string filter)
        {
            foreach(var role in UserAuthorizationManager.userRoles)
            {
                if(role.Name== filter)
                {
                    this._role = role;
                    return;
                }
            }

            foreach(var permission in UserAuthorizationManager.userPermissions)
            {
                if(permission.Name== filter)
                {
                    this._permission = permission;
                    return;
                }
            }
        }

        
        public UserAuthorizationAttribute(UserPermission permission)
        {
            _permission = permission;
        }
         
        public UserAuthorizationAttribute(UserRole role)
        {
            _role = role;
        }
        
        /// <summary>
        /// Check authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var currentUser = HttpContext.Current.User;

            bool isAuthorized = false;

            if(_permission!= null)
            {
                if(currentUser.HasPermission(_permission))
                {
                    isAuthorized = true;
                }
            }

            if(_role !=null )
            {
                if(currentUser.HasRole(_role))
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized == false)
            {
                filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "Home", action = "AccessDenied" }));
            }
        }
    }
}