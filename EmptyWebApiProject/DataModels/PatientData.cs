﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;

namespace Healthee.DataModels
{
    public class PatientData
    {
        public Person Person { get; set; }
        public int PatientID { get; set; }
        public int MedicalRecordID { get; set; }
        public string Allergies { get; set; }
        public string Medication { get; set; }
        public string Notes { get; set; }
    }
}