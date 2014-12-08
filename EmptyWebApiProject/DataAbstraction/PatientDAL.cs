using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;
using Healthee.DataModels;

namespace Healthee.DataAbstraction
{
    public class PatientDAL : AbstractDAL
    { 

        /// <summary>
        /// Returns patient data using patient id 
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public static PatientData GetPatientData(int patientID)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var query = from person in db.People
                            join patients in db.Patients on person.PersonID equals patients.PersonID
                            join medrecords in db.MedicalRecords on patients.MedicalRecordID equals medrecords.MedicalRecordID
                            where patients.PatientID == patientID
                            select new PatientData()
                            {
                                Person = person,
                                PatientID = patients.PatientID,
                                MedicalRecordID = patients.MedicalRecordID,
                                Allergies = medrecords.Allergies,
                                Medication = medrecords.Medication,
                                Notes = medrecords.Notes
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
                            join medrecords in db.MedicalRecords on patients.MedicalRecordID equals medrecords.MedicalRecordID
                            where (string.IsNullOrEmpty(firstName) ? true : p.FirstName == firstName)
                            && (string.IsNullOrEmpty(lastName) ? true : p.LastName == lastName)
                            && (string.IsNullOrEmpty(nationalID) ? true : p.NationalID == nationalID)
                            && (string.IsNullOrEmpty(mobileNumber) ? true : p.MobileNumber == mobileNumber)
                            select new PatientData()
                            {
                                Person = p,
                                PatientID = patients.PatientID,
                                MedicalRecordID = patients.MedicalRecordID,
                                Allergies = medrecords.Allergies,
                                Medication = medrecords.Medication,
                                Notes = medrecords.Notes
                            };
                return query.ToList<PatientData>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Inserts a row into patient table 
        /// </summary>
        /// <param name="personid"></param>
        /// <param name="medicalrecordid"></param>
        /// <returns></returns>
        public static int InsertPatient(int personid, int medicalrecordid)
        { 
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                Patient p = new Patient
                {
                    PersonID = personid,
                    MedicalRecordID = medicalrecordid
                };

                db.Patients.Add(p);
                db.SaveChanges();
                return p.PatientID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        
    }
}