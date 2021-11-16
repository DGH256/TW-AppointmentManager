using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public static class HelperClass
    {
        static Random rand = new Random();

        public static string GenerateRandomString(int length)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";

            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rand.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}