using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataAbstraction;
using Healthee.DataModels;
using Newtonsoft.Json.Linq; 

namespace Healthee.Controllers
{
    public class PatientController : ApiController
    {
        /// <summary>
        /// Get patient data with passed patient id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public PatientData GetPatientData([FromBody]JToken value)
        {
            int docId = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out docId);
            if (DoctorDAL.DoctorExists(docId))
            {
                int patid = 0; 
                int.TryParse((string)value.SelectToken("patientid"), out patid);
                return DataService.GetPatientData(patid);
            }
            return null; 
        }

        /// <summary>
        /// Passes params to search for patient data 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PatientData> SearchPatientData([FromBody]JToken value)
        {
            int docId = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out docId);

            if (DoctorDAL.DoctorExists(docId))
            {
                List<PatientData> patients = DataService.GetPatients((string)value.SelectToken("firstname"),
                                                   (string)value.SelectToken("lastname"),
                                                   (string)value.SelectToken("nationalid"),
                                                   (string)value.SelectToken("mobilenumber")
                                              );
                if (patients == null)
                    UserActivity.AddDoctorActivity((int)ActivityEnum.PatientSearch, docId, (int)StatusEnum.Failure, "No patients found", value.ToString());
                else
                {
                    UserActivity.AddDoctorActivity((int)ActivityEnum.PatientSearch, docId, (int)StatusEnum.Success, "Success", value.ToString());
                    return patients; 
                }                 
            }
            return null; 
        }
         
    }
}
