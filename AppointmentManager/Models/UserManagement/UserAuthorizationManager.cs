using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using AppointmentManager;
using AppointmentManager.UserManagement;
using System.Web.Security;

namespace AppointmentManager
{
    public static class UserAuthorizationManager
    {
        public static SortedSet<UserRole> userRoles = new SortedSet<UserRole>();

        private static List<AspNetRole> dbRoles = new List<AspNetRole>();

        public static HashSet<UserPermission> userPermissions = new HashSet<UserPermission>();

        public static int defaultIdLength = 40;

        private static bool addedDbRoles = false;

        public static void AddRole(UserRole role)
        {
            if(!addedDbRoles)
            {
                using (dbContext context = new dbContext())
                {
                    dbRoles = context.AspNetRoles.ToList();
                }

                addedDbRoles = true;
            }

            if (!userRoles.Contains(role))
            {
                var dbRole = dbRoles.Where(p => p.Name == role.Name).FirstOrDefault();

                if(dbRole!=null)
                {
                    role.Id = dbRole.Id;
                }

                userRoles.Add(role);
            }

        }
  
        public static void AddPermission( UserPermission  permission )
        {
            if (!userPermissions.Contains(permission))
            {
                userPermissions.Add(permission);
            }
        }

        public static UserPermission getPermission( string name )
        {
            var permission = userPermissions.Where(p => p.Name == name).FirstOrDefault();

            return permission;
        }

        public static string GetUserId(IPrincipal user)
        {
            var cacheResult = UserCacheDictionary.getUserId(user.Identity.Name);

            if (cacheResult != null)
            {
                return cacheResult;
            }

            string result = null;

            try
            {
                using (dbContext context = new dbContext())
                {
                    var dbuser = context.AspNetUsers.Where(p => p.UserName == user.Identity.Name).FirstOrDefault();

                    if (dbuser == null) return null;

                    result = dbuser.Id;

                    UserCacheDictionary.addUserInfo(user,null,null,result);
                }
            }

            catch(Exception ex) { DebugInfo.Log(ex.Message); }

            return result;
        }

        public static List<string> GetUserRoles(IPrincipal user)
        {

            List<string> results = new List<string>();

            if (!user.Identity.IsAuthenticated) { return results; }
            try
            {
                List<AspNetRole> allRoles = new List<AspNetRole>();

                using (dbContext context = new dbContext())
                {
                    var currentUser = context.AspNetUsers.Where(p => p.Email == user.Identity.Name).FirstOrDefault();

                    allRoles = currentUser.AspNetRoles.ToList();

                    foreach (var role in allRoles)
                    {
                        results.Add(role.Name);
                    }
                }
            }
            catch (Exception ex) { DebugInfo.Log(ex.Message); }

            return results;
        }

        public static bool UserHasPermission( IPrincipal user , UserPermission permission )
        {
            return UserHasPermission(user, permission.Name);
        }

        public static bool UserHasPermission( IPrincipal user, string permission)
        {
            var cacheResult = UserCacheDictionary.userHasPermission(user.Identity.Name, permission);

            if (cacheResult != null) { return (bool)(cacheResult); }

            UserRole currentRole = null;

            for (int i = userRoles.Count - 1; i >= 0; i--)
            {
                currentRole = userRoles.ElementAt(i);

                if (user.IsInRole(currentRole.Name))
                {
                    UserCacheDictionary.addUserInfo(user, currentRole.Name, null, null);

                    foreach(var currentUserPermission in currentRole.Permissions)
                    {
                        if(currentUserPermission.Name==permission)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool UserHasRole(IPrincipal user, string role)
        {
            var cacheResult = UserCacheDictionary.userHasRole(user.Identity.Name,role);

            if(cacheResult!= null)
            {
                return (bool)(cacheResult);
            }

            try
            {
                using (dbContext context = new dbContext())
                {
                    var dbUser = context.AspNetUsers.Where(p => p.Email == user.Identity.Name).First();

                    var dbRole = context.AspNetRoles.Where(p => p.Name == role).First();

                    if (dbUser.AspNetRoles.Contains(dbRole))
                    {
                        UserCacheDictionary.addUserInfo(user, role, null, null);

                        return true;
                    }

                }
            }
            catch(Exception ex)
            {
                DebugInfo.Log(ex.Message);
            }


            return false;
        }

        public static bool UserHasRole(IPrincipal user, UserRole role)
        {
            return UserHasRole(user, role.Name);
        }

        public static bool UserHasAuthority(IPrincipal user, UserRole role)
        {
            UserRole currentRole = null;

            for (int i = 0; i < userRoles.Count; i++)
            {
                currentRole = userRoles.ElementAt(i);

                if (currentRole.AuthorityLevel < role.AuthorityLevel)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool AssignUserToRole(IPrincipal user, string roleName)
        {
            try
            {
                if (user.Identity.IsAuthenticated)
                {
                    using (dbContext context = new dbContext())
                    {
                        var dbUser = context.AspNetUsers.Where(p => p.UserName == user.Identity.Name).First();

                        var dbRole = context.AspNetRoles.Where(p => p.Name == roleName).First();

                        dbUser.AspNetRoles.Add(dbRole);

                        UserCacheDictionary.addUserInfo(user, roleName, null, null);

                        context.SaveChanges();
                    }
                }
            }

            catch(Exception ex)
            {
                DebugInfo.Log(ex.Message);

                return false;
            }

            return true;
        }
    }
}