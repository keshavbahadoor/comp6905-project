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
            //int docId = 0;
            //int.TryParse((string)value.SelectToken("doctorid"), out docId);
            //if (DoctorDAL.DoctorExists(docId))
            //{
                int patid = 0; 
                int.TryParse((string)value.SelectToken("patientid"), out patid);
                return PatientDAL.GetPatientData(patid);
            //}
            //return null; 
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
                List<PatientData> patients = PatientDAL.GetPatients((string)value.SelectToken("firstname"),
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
        /// <summary>
        /// Creates a new patient record 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Message AddRecord([FromBody]JToken value)
        {
            int doctorid = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out doctorid);
            if (!DoctorDAL.DoctorExists(doctorid))
            {
                return MessageHandler.Error("Incorrect doctorid. Not authorized to add patients");
            }          

            string firstname = (string)value.SelectToken("firstname");
            string lastname = (string)value.SelectToken("lastname");
            string nationalid = (string)value.SelectToken("nationalid");
            string gender = (string)value.SelectToken("gender"); 
            string allergies = (string)value.SelectToken("allergies");
            string medication = (string)value.SelectToken("medication");

            DateTime dob = DateTime.MinValue;
            //if (! DateTime.TryParse((string)value.SelectToken("dateofbirth"), out dob))
            //{
            //    dob = DateTime.MinValue;
            //}
            
            if (firstname == null) return MessageHandler.Error("You must enter a first name");
            if (lastname == null) return MessageHandler.Error("You must enter a last name");
            if (nationalid == null) return MessageHandler.Error("You must enter a valid National Identification number");
           
            // Add person 
            int personID = PersonDAL.InsertPerson(firstname, lastname, gender, dob, nationalid,
                                                    (string)value.SelectToken("mobilenumber"),
                                                    (string)value.SelectToken("homenumber"),
                                                    (string)value.SelectToken("worknumber"),
                                                    (string)value.SelectToken("address1"),
                                                    (string)value.SelectToken("address2"),
                                                    (string)value.SelectToken("city"),
                                                    (string)value.SelectToken("country")
                );
             
            // Add medical record 
            int medicalRecordId = MedicalDAL.InsertMedicalRecord(allergies, medication, (string)value.SelectToken("notes"));

            // Add patient mapping 
            PatientDAL.InsertPatient(personID, medicalRecordId); 

            UserActivity.AddDoctorActivity((int)ActivityEnum.PatientRegistratoin, doctorid, (int)StatusEnum.Success, "Success", value.ToString());
            return MessageHandler.Success("You have successfully registered a new patient on Healthee!");
        }
        /// <summary>
        /// Updates patient record 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public Message UpdateRecord([FromBody]JToken value)
        {
            int doctorid = 0, personid = 0, medicalrecordid = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out doctorid);
            int.TryParse((string)value.SelectToken("personid"), out personid);
            int.TryParse((string)value.SelectToken("medicalrecordid"), out medicalrecordid);
            if (!DoctorDAL.DoctorExists(doctorid))
            {
                return MessageHandler.Error("Incorrect doctorid. Not authorized to update patients");
            }
            if (!PersonDAL.PersonExist(personid))
            {
                return MessageHandler.Error("Incorrect personid. Patient not found!");
            }
            if (!MedicalDAL.RecordsExist(medicalrecordid))
            {
                return MessageHandler.Error("Incorrect medicalrecordid. Records not found!");
            }             

            string firstname = (string)value.SelectToken("firstname");
            string lastname = (string)value.SelectToken("lastname");
            string nationalid = (string)value.SelectToken("nationalid");
            string gender = (string)value.SelectToken("gender");
            string allergies = (string)value.SelectToken("allergies");
            string medication = (string)value.SelectToken("medication");

            DateTime dob = DateTime.MinValue;
            //if (! DateTime.TryParse((string)value.SelectToken("dateofbirth"), out dob))
            //{
            //    dob = DateTime.MinValue;
            //}

            if (firstname == null) return MessageHandler.Error("You must enter a first name");
            if (lastname == null) return MessageHandler.Error("You must enter a last name");
            if (nationalid == null) return MessageHandler.Error("You must enter a valid National Identification number");

            // Update person record 
            PersonDAL.UpdatePerson(personid, firstname, lastname, gender, dob, nationalid, 
                                                    (string)value.SelectToken("mobilenumber"),
                                                    (string)value.SelectToken("homenumber"),
                                                    (string)value.SelectToken("worknumber"),
                                                    (string)value.SelectToken("address1"),
                                                    (string)value.SelectToken("address2"),
                                                    (string)value.SelectToken("city"),
                                                    (string)value.SelectToken("country"));
            // Update medical record 
            MedicalDAL.UpdateMedicalRecord(medicalrecordid,
                                            (string)value.SelectToken("allergies"),
                                            (string)value.SelectToken("medication"),
                                            (string)value.SelectToken("notes"));

            UserActivity.AddDoctorActivity((int)ActivityEnum.UpdateProfile, doctorid, (int)StatusEnum.Success, "Success", value.ToString());
            return MessageHandler.Success("Patient records updated successfully.");
        }
         
    }
}
