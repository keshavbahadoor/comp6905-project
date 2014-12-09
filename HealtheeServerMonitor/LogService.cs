using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HealtheeServerMonitor
{
    class LogService
    {
        /// <summary>
        /// Service start 
        /// </summary>
        public static void ServiceStart()
        {
            using (StreamWriter log = File.AppendText(Properties.Settings.Default.LogFile))
            {
                log.WriteLine(  "--------------------------------------------------");
                log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - Health Montior Service Start");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - Health Montior Service Start");
                Console.WriteLine("--------------------------------------------------");
            }
        }
        /// <summary>
        /// service end 
        /// </summary>
        public static void ServiceEnd()
        {
            using (StreamWriter log = File.AppendText(Properties.Settings.Default.LogFile))
            {                
                log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - Health Montior Service End");
                log.WriteLine("--------------------------------------------------");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - Health Montior Service End");
                Console.WriteLine("--------------------------------------------------");
            }
        }
        /// <summary>
        /// default log message 
        /// </summary>
        /// <param name="msg"></param>
        public static void LogMsg(string msg)
        {
            using (StreamWriter log = File.AppendText(Properties.Settings.Default.LogFile))
            {
                log.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + msg); 
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + msg); 
            }
        }
    }
}
