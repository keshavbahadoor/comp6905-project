using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthee.Models
{
    /// <summary>
    ///  basic message structure 
    /// </summary>
    public class Message
    {
        public string type { get; set; }
        public string message { get; set; }
    }
    /// <summary>
    /// Defines a handler for sending error and success messages 
    /// </summary>
    public static class MessageHandler
    {
        public static Message Error(string msg)
        {
            return new Message { type = "error", message = msg };
        }
        public static Message Success(string msg)
        {
            return new Message { type = "success", message = msg };
        }
    }
}