using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataAbstraction;
using Newtonsoft.Json.Linq;
using Healthee.DataModels;
namespace Healthee.Controllers
{
    public class DoctorController : ApiController
    {
        /// <summary>
        /// Gets doctor types 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<DoctorType> GetDoctorTypes()
        {
            return DoctorDAL.GetDoctorTypes();
        }
        /// <summary>
        /// REturns doctor data 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public DoctorData GetDoctorData([FromBody]JToken value)
        {
            int docId = 0;
            int.TryParse((string)value.SelectToken("doctorid"), out docId);

            return DoctorDAL.GetDoctorData(docId);
        }
        /// <summary>
        /// Passes params to search for doctor data 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<DoctorData> SearchDoctorData([FromBody]JToken value)
        {
            List<DoctorData> doctors = DoctorDAL.GetDoctors((string)value.SelectToken("firstname"),
                                                (string)value.SelectToken("lastname"),
                                                (string)value.SelectToken("city")
                                            );
            return doctors;
        }
        /// <summary>
        /// registers a new doctor 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Message Register([FromBody]JToken value)
        {
            int doctypeId = 0;
            int.TryParse((string)value.SelectToken("doctortypeid"), out doctypeId);

            string firstname = (string)value.SelectToken("firstname");
            string lastname = (string)value.SelectToken("lastname");
            string nationalid = (string)value.SelectToken("nationalid");
            string gender = (string)value.SelectToken("gender");
            string username = (string)value.SelectToken("username");
            string password = (string)value.SelectToken("password");

            DateTime dob = DateTime.Now;
            DateTime.TryParse((string)value.SelectToken("dateofbirth"), out dob);

            if (doctypeId == 0) return MessageHandler.Error("Please select doctor type");
            if (firstname == null) return MessageHandler.Error("You must enter a first name");
            if (lastname == null) return MessageHandler.Error("You must enter a last name");
            if (nationalid == null) return MessageHandler.Error("You must enter a valid National Identification number");
            if (username == null) return MessageHandler.Error("You must enter a username");
            if (password == null) return MessageHandler.Error("You must enter a password");

            int personID = PersonDAL.InsertPerson(firstname, lastname, gender, dob, nationalid,
                                                    (string)value.SelectToken("mobilenumber"),
                                                    (string)value.SelectToken("homenumber"),
                                                    (string)value.SelectToken("worknumber"),
                                                    (string)value.SelectToken("address1"),
                                                    (string)value.SelectToken("address2"),
                                                    (string)value.SelectToken("city"),
                                                    (string)value.SelectToken("country"),
                                                    (string)value.SelectToken("email")
                );

            int doctorid = DoctorDAL.InsertDoctor(personID, doctypeId, username, password);

            // Add user activity 
            UserActivity.AddDoctorActivity((int)ActivityEnum.DoctorRegistration, doctorid, (int)StatusEnum.Success, "Success", value.ToString());

            // Email registration 
            MailService.SendRegistrationEmail((string)value.SelectToken("email"));

            // Return success
            return MessageHandler.Success("You have successfully registered on Healthee!");
        }


    }
}