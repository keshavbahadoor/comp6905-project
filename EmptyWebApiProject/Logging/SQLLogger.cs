using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO; 

namespace Healthee.Logging
{
    /// <summary>
    /// Logs Entity framework to file 
    /// </summary>
    public class SQLLogger
    {
        /// <summary>
        /// Log to file 
        /// </summary>
        /// <param name="line"></param>
        public static void LogToFile(string line)
        {
            using (StreamWriter writer = File.AppendText(Properties.Settings.Default.RequestLogFile))
            {
                writer.WriteLine(line);
            }
        }

    }
}