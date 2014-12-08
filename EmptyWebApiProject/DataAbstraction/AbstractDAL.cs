using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthee.DataAbstraction
{
    /// <summary>
    /// Abstract data access layer class 
    /// </summary>
    public abstract class AbstractDAL
    {
        /// <summary>
        /// Enables debugging - outputs LINQ query 
        /// </summary>
        public static bool DEBUG = false;
    }
}