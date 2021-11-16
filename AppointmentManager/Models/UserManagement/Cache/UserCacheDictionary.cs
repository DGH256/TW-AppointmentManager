using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AppointmentManager.UserManagement
{
    public static class UserCacheDictionary
    {
        private static Dictionary<string, UserCacheItem> dictionary = new Dictionary<string, UserCacheItem>();

        public static bool hasUser(string username)
        {
            return dictionary.ContainsKey(username);
        }

        public static bool? userHasRole(string username, string rolename)
        {
            if (!hasUser(username) || dictionary[username].roles==null)
            {
                return null;
            }
            else
            {
                return dictionary[username].roles.Contains(rolename);
            }
        }

        public static bool? userHasPermission(string username, UserPermission permission)
        {
            if(!hasUser(username) || dictionary[username].permissions==null)
            {
                return null;
            }
            else
            {
                return dictionary[username].permissions.Contains(permission.Name);
            }
        }

        public static bool? userHasPermission(string username, string permission)
        {
            if (!hasUser(username) || dictionary[username].permissions == null)
            {
                return null;
            }
            else
            {
                return dictionary[username].permissions.Contains(permission);
            }
        }

        public static string getUserId(string username)
        {
            if(!hasUser(username) || dictionary[username].Id==null)
            {
                return null;
            }
            else
            {
                return dictionary[username].Id;
            }
        }

        public static void addUserInfo(IPrincipal user, string role, string permission, string userId)
        {
            addUserInfo(user.Identity.Name, role, permission, userId);
        }

        public static void addUserInfo(string username, string role, string permission, string userId)
        {
            createIfNotExists(username);

            if(userId!= null)
            {
                dictionary[username].Id = userId;
            }

            if(role!= null)
            {
                if (dictionary[username].roles == null) dictionary[username].roles = new HashSet<string>();

                dictionary[username].roles.Add(role);
            }

            if (permission != null)
            {
                if (dictionary[username].permissions == null) dictionary[username].permissions = new HashSet<string>();

                dictionary[username].permissions.Add(permission);
            }

        }

        private static void createIfNotExists(string username)
        {
            if (!dictionary.ContainsKey(username))
            {
                dictionary.Add(username, new UserCacheItem());
            }
        }
    }
}