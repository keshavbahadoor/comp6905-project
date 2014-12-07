using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; 
using System.Web;
using System.Net.Http;
using System.Threading; 
using System.Threading.Tasks; 

namespace Healthee.Logging
{
    /// <summary>
    /// Logs all requests and responses that hit the server 
    /// </summary>
    public class RequestResponseLogger : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //logging request body
            string requestBody =   request.Content.ReadAsStringAsync().Result;
            string headers = request.Content.Headers.ToString();            


            LogToFile("Request Parameters: " + requestBody);
            LogToFile("Header: " + headers); 

            //let other handlers process the request
            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    //once response is ready, log it
                    var responseBody = task.Result.Content;
                   // LogToFile((string)responseBody.ToString()); 

                    return task.Result;
                });
        }
        /// <summary>
        /// Logs to log file 
        /// </summary>
        public static void LogToFile(string line)
        {
            using (StreamWriter writer = File.AppendText(Properties.Settings.Default.RequestLogFile))
            {
                writer.WriteLine(line);
            }
        }


        /// <summary>
        /// Creates directory if it does not exists 
        /// </summary> 
        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

    }

    

}