using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(AppointmentManager.Startup))]
namespace AppointmentManager
{
    public partial class Startup
    {
        private static bool performedSanityCheck = false;

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            if (performedSanityCheck == false)
            {
                PerformSanityChecks();

                performedSanityCheck = true;
            }
        }


        /// <summary>
        /// Making sure that everything is initialized, that all our permissions/userRoles exist in the database
        /// </summary>
        private void PerformSanityChecks()
        {
            AuthOptions.Initialize();

            using (dbContext context = new dbContext())
            {
                foreach (var role in UserAuthorizationManager.userRoles)
                {
                    try
                    {
                        var existsInDb = context.AspNetRoles.Where(p => p.Name == role.Name).Count();

                        if (existsInDb == 0)
                        {
                            context.AspNetRoles.Add(new AspNetRole() { Id = role.Id, Name = role.Name });

                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
