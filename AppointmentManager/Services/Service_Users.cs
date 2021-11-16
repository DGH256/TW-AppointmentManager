using AppointmentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AppointmentManager
{
    public static class Service_Users
    {
        public static AspNetUser GetUserSettings(IPrincipal user)
        {
            var userId = user.GetId();

            using (dbContext context = new dbContext())
            {
                return context.AspNetUsers.Where(p => p.Id == userId).FirstOrDefault();
            }
        }

        public static void SetPersonalSettings(IPrincipal user, AspNetUser settings)
        {
            var userId = user.GetId();

            using (dbContext context = new dbContext())
            {
                var currentSettings = context.AspNetUsers.Where(p => p.Id == userId).FirstOrDefault();

                currentSettings.CopyProperties(settings);

                context.SaveChanges();
            }
        }

        public static string GetUserNickname(IPrincipal user)
        {
            if(user.Identity.IsAuthenticated)
            {
                var currentSettings = GetUserSettings(user);

                if (currentSettings != null)
                {
                    return currentSettings.Nickname;
                }
            }

            return user.Identity.Name;
        }

        public static List<AspNetUser> GetAllUsers()
        {
            List<AspNetUser> allUsers = new List<AspNetUser>();

            using (dbContext context = new dbContext())
            {
              allUsers = context.AspNetUsers.Include("AspNetRoles").Where(p => p.isDeleted == false).ToList();
            }

            return allUsers;            
        }

        public static void DeleteUser(string id)
        {
            using (dbContext context = new dbContext())
            {
                var user = context.AspNetUsers.Where(p => p.Id == id).FirstOrDefault();

                user.isDeleted = true;

                context.SaveChanges();
            }
        }

        public static bool UpdateUserSettings(AspNetUser newSettings, string roleList = null)
        {
            try
            {
                using (dbContext context = new dbContext())
                {
                    var oldSettings = context.AspNetUsers.Where(p => p.Id == newSettings.Id).FirstOrDefault();

                    if (roleList != null)
                    {
                        var currentUserRoles = oldSettings.AspNetRoles.ToList();

                        var allRoles = context.AspNetRoles.ToList();

                        //Checking if any roles have been removed from the user
                        var roles = roleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < roles.Count(); i++)
                        {
                            roles[i] = roles[i].Trim();
                        }

                        foreach (var currentRole in currentUserRoles)
                        {
                            if (!roles.Contains(currentRole.Name))
                            {
                                oldSettings.AspNetRoles.Remove(currentRole);
                            }
                        }

                        //Checking if any new roles have been added to the user
                        foreach (var role in roles)
                        {
                            var userHasRole = currentUserRoles.Where(p => p.Name == role).FirstOrDefault();

                            //If the user doesn't have this role already
                            if (userHasRole == null)
                            {
                                var roleToAdd = allRoles.Where(p => p.Name == role).FirstOrDefault();
                                if (roleToAdd != null)
                                {
                                    oldSettings.AspNetRoles.Add(roleToAdd);
                                }
                            }
                        }
                    }

                    oldSettings.CopyProperties(newSettings);

                    context.SaveChanges();

                    return true;
                }
            }

            catch (Exception ex)
            {
                DebugInfo.Log(ex.Message);
            }

            return false;
        }
    }
}