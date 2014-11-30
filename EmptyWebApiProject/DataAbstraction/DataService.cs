using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using Healthee.EFModels;
using Healthee.DataModels;

namespace Healthee.DataAbstraction
{
    /// <summary>
    /// Basic data abstraciton layer that is used inbetween controllers and 
    /// Database 
    /// Uses Entity Framework 6  
    /// </summary>
    public class DataService
    {
        public static bool DEBUG = false;
        
        public static DoctorType GetDoctorType(int id)
        { 
            HealtheeEntities db = new HealtheeEntities();
            if (DEBUG) db.Database.Log = Console.WriteLine; 

            var query = from dt in db.DoctorTypes
                        where dt.DoctorTypeID == id
                        select dt;
            DoctorType dType = query.Single();
            return dType;
        }
        /// <summary>
        /// Returns patient data using patient id 
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public static PatientData GetPatientData (int patientID)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine; 

                var query = from person in db.People
                            join patients in db.Patients on person.PersonID equals patients.PersonID
                            where patients.PatientID == patientID  
                            select new PatientData()
                            {
                                Person = person,
                                PatientID = patients.PatientID,
                                MedicalRecordID = patients.MedicalRecordID
                            };
                PatientData data = query.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        /// <summary>
        /// Basic search functionality
        /// - returns patient data in list form 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="nationalID"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static List<PatientData> GetPatients(string firstName, string lastName, string nationalID, string mobileNumber)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine; 

                var query = from p in db.People
                            join patients in db.Patients on p.PersonID equals patients.PersonID
                            where  (string.IsNullOrEmpty(firstName) ? true : p.FirstName == firstName)
                            && (string.IsNullOrEmpty(lastName) ? true : p.LastName == lastName)
                            && (string.IsNullOrEmpty(nationalID) ? true : p.NationalID == nationalID)
                            && (string.IsNullOrEmpty(mobileNumber) ? true : p.MobileNumber == mobileNumber)
                            select new PatientData()
                            {
                                Person = p,
                                PatientID = patients.PatientID,
                                MedicalRecordID = patients.MedicalRecordID
                            };
                return query.ToList<PatientData>(); 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Inserts row do Activity table 
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="doctorID"></param>
        /// <param name="statusID"></param>
        /// <param name="details"></param>
        /// <param name="data"></param>
        public static void AddDoctorActivity(int activityType, int doctorID, int statusID, string details, string data)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                db.Activities.Add(new Activity()
                {
                    ActivityTypeID = activityType,
                    DoctorID = doctorID, 
                    StatusID = statusID, 
                    Details = details, 
                    Data = data,
                    ActivityDate = DateTime.Now
                });
                db.SaveChanges(); 
            }
            catch (Exception e)
            {
                // log exception 
            }
        }
        /// <summary>
        /// Checks if a doctor exists on the database
        /// Returns true or false if otherwise 
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns></returns>
        public static bool DoctorExists(int doctorID)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var count = (from d in db.Doctors
                             where d.DoctorID == doctorID
                             select d).Count();
                if (count > 0) return true;
                return false; 
            }
            catch(Exception e)
            {
                return false; 
            }
        }


    }
}