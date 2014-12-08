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
    public class AppointmentController : ApiController
    {
        /// <summary>
        /// Creates a new appointment 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Message AddAppointment([FromBody]JToken value)
        {
            int doctorid = 0, patientid = 0 ;
            int.TryParse((string)value.SelectToken("doctorid"), out doctorid);
            int.TryParse((string)value.SelectToken("patientid"), out patientid); 
            if (!DoctorDAL.DoctorExists(doctorid))
            {
                return MessageHandler.Error("Incorrect doctorid. Not authorized to update patients");
            }
            if (!PatientDAL.PatientExist(patientid))
            {
                return MessageHandler.Error("Incorrect patientid. Patient not found!");
            }
            string date = (string)value.SelectToken("date");
            string time = (string)value.SelectToken("time");
            if (date == null) return MessageHandler.Error("Please specify a date for this appointment.");
            if (time == null) return MessageHandler.Error("Please specify a time for this appointment.");

            AppointmentDAL.InsertAppointment(doctorid, patientid, (int)StatusEnum.Success, date, time, 
                                                (string)value.SelectToken("notes") );

            UserActivity.AddDoctorActivity((int)ActivityEnum.CreateAppointment, doctorid, (int)StatusEnum.Success, "Success", value.ToString());
            return MessageHandler.Success("Appointment added!");
        }

        /// <summary>
        /// returns doctor appointments 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Appointment> GetDoctorAppointments([FromBody]JToken value)
        {
            int docId = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out docId);
            if (DoctorDAL.DoctorExists(docId))
            {
                UserActivity.AddDoctorActivity((int)ActivityEnum.ViewAppointments, docId, (int)StatusEnum.Success, "Success", value.ToString());
                return AppointmentDAL.GetDoctorAppointments(docId); 
            }
            return null;
        }
        /// <summary>
        /// returns patient appointments 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Appointment> GetPatientAppointments([FromBody]JToken value)
        {
            int patientid = 0;
            int.TryParse((string)value.SelectToken("patientid"), out patientid);
            if (PatientDAL.PatientExist(patientid))
            {
                return AppointmentDAL.GetPatientAppointments(patientid);
            }
            return null;
        }

    }
}
