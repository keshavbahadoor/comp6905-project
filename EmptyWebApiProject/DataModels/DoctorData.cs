using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;


namespace Healthee.DataModels
{
    public class DoctorData
    {
        public Person Person { get; set; }
        public int DoctorID { get; set; }
        public string DoctorType  { get; set; }
        public string Username { get; set; }

    }
}