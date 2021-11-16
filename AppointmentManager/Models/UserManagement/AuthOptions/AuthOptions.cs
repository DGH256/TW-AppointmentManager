using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppointmentManager.UserManagement;

namespace AppointmentManager
{
    public static class AuthOptions
    {
        private static bool is_initialized = false;

        public static void Initialize()
        {
            if (is_initialized == false)
            {
                Permissions.Initialize();
                Roles.Initialize();
                is_initialized = true;
            }
        }

        public static class Permissions
        {
            private static bool is_initialized = false;

            public const string AddRegularUser = "AddRegularUser";

            public const string ViewCustomerAppointments = "ViewCustomerAppointments";

            public const string EditCustomerAppointments = "EditCustomerAppointments";

            public const string ViewAccountingSection = "ViewAccountingSection";

            public const string EditCustomerInfo = "EditCustomerInfo";

            public const string EditUserInfo = "EditUserInfo";

            public const string ViewCustomersPage = "ViewCustomersPage";

            public const string ViewUsersPage = "ViewUsersPage";

            public const string ViewCalendarPage = "ViewCalendarPage";

            public static void Initialize()
            {
                if(is_initialized == false)
                {
                   new UserPermission(ViewAccountingSection);
                   new UserPermission(EditCustomerAppointments);
                   new UserPermission(ViewCustomerAppointments);
                   new UserPermission(AddRegularUser);
                   new UserPermission(EditCustomerInfo);
                   new UserPermission(EditUserInfo);
                   new UserPermission(ViewCustomersPage);
                   new UserPermission(ViewUsersPage);
                   new UserPermission(ViewCalendarPage);

                    is_initialized = true;
                }
            }
        }

        public class Roles
        {
            private static bool is_initialized = false;

            public const string SuperAdmin = "Super Admin";

            public const string FacilityAdmin = "Organization Admin";

            public const string RegularUser = "Regular User";

            public static void Initialize()
            {
                if (is_initialized == false )
                {
                    new UserRole(SuperAdmin, 0, new[] {
                        Permissions.ViewCalendarPage, Permissions.ViewCustomersPage, Permissions.ViewUsersPage,
                        Permissions.EditCustomerInfo, Permissions.EditUserInfo
                    });


                    new UserRole(FacilityAdmin, 1, new[] {
                        Permissions.ViewCalendarPage, Permissions.ViewCustomersPage, Permissions.ViewUsersPage,
                        Permissions.EditCustomerInfo
                    });

                    new UserRole(RegularUser, 2, new[] {
                        Permissions.ViewCalendarPage
                    });


                    is_initialized = true;
                }
            }
            
        }
    }

    
}