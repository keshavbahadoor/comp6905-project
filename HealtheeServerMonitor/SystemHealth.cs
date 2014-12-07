using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 

namespace HealtheeServerMonitor
{
    /// <summary>
    /// performs local system resource checks 
    /// </summary>
    class SystemHealth
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public SystemHealth()
        {
            // init counters 
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 
        }

        public float GetAvailableRAM()
        {
            return ramCounter.NextValue(); 
        }
        public float GetCPUUsage()
        {
            return cpuCounter.NextValue();
        }
    }
}
