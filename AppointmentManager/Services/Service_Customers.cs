using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public static class Service_Customers
    {
        public static List<Customer> GetCustomers()
        {
            using (dbContext context = new dbContext())
            {
                return context.Customers.Where(p=>p.isDeleted== false).ToList();
            }
        }

        public static void AddCustomer(Customer customer)
        {
            using (dbContext context = new dbContext())
            {
                context.Customers.Add(customer);

                context.SaveChanges();
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (dbContext context = new dbContext())
            {
                var oldCustomer = context.Customers.Where(p => p.Id == customer.Id).FirstOrDefault();

                oldCustomer.CopyProperties(customer);

                context.SaveChanges();
            }
        }

        public static void DeleteCustomer(int id)
        {
            using (dbContext context = new dbContext())
            {
                var customer = context.Customers.Where(p => p.Id == id).FirstOrDefault();

                customer.isDeleted = true;

                context.SaveChanges();
            }
        }
    }
}