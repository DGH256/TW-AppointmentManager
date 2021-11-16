using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AppointmentManager
{
    public static class DebugInfo
    {
        private static string savePath = Directory.GetCurrentDirectory() + @"\logs.txt";

        public static void Log(string str)
        {
            //using (StreamWriter sw = File.AppendText(savePath))
            //{
            //    sw.Write(str);
            //}
        }
    }
}