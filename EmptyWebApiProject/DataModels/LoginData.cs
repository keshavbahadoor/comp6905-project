using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthee.DataModels
{
    public class LoginData
    {
        public int doctorid { get; set; }
        public int patientid { get; set; }
        public bool loggedin { get; set; }
    }
}