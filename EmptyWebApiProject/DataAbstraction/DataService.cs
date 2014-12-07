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
        /// Inserts person data into database 
        /// returns last inserted id 
        /// </summary>
        /// <returns></returns>
        public static int InsertPerson(string firstname, string lastname, string gender, DateTime dob, string nationalid, string mobilenumber,
                                       string homenumber, string worknumber, string address1, string address2, string city, string country)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                Person p = new Person { 
                        FirstName = firstname, 
                        LastName = lastname, 
                        Gender = gender,
                        //DateOfBirth = dob,
                        NationalID = nationalid,
                        MobileNumber = mobilenumber,
                        HomeNumber = homenumber,
                        WorkNumber = worknumber,
                        Address1 = address1, 
                        Address2 = address2,
                        City = city,
                        Country = country
                };

                db.People.Add( p);
                db.SaveChanges();
                return p.PersonID;
            }
            catch(Exception e)
            {
                return 0;
            }
        }


    }
}