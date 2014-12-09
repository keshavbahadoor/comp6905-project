using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 

namespace HealtheeServerMonitor
{
    class Program
    {
        /// <summary>
        /// monitors server resource usage
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            LogService.ServiceStart(); 

            SystemHealth systemHealth = new SystemHealth();

            LogService.LogMsg("Available System RAM > " + systemHealth.GetAvailableRAM() + " MB");
            LogService.LogMsg("Available System CPU > " + systemHealth.GetCPUUsage() + " %");

            LogService.LogMsg("Server Ping Test - Available? > " + PingService.PingHost(Properties.Settings.Default.serveraddress)); 

            LogService.ServiceEnd(); 
            Console.ReadLine();
        }
    }
}
