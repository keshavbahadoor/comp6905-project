using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;
using Healthee.DataModels;

namespace Healthee.DataAbstraction
{
    public class MedicalDAL : AbstractDAL
    { 

        /// <summary>
        /// Inserts a medical record  
        /// </summary>
        /// <param name="allergies"></param>
        /// <param name="medication"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public static int InsertMedicalRecord(string allergies, string medication, string notes)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                MedicalRecord m = new MedicalRecord
                {
                    Allergies = allergies, 
                    Medication = medication, 
                    Notes = notes
                };

                db.MedicalRecords.Add(m);
                db.SaveChanges();
                return m.MedicalRecordID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// Updates medical record 
        /// </summary>
        /// <param name="allergies"></param>
        /// <param name="medication"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public static bool UpdateMedicalRecord(int medicalrecordid, string allergies, string medication, string notes)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var query = from med in db.MedicalRecords
                            where med.MedicalRecordID == medicalrecordid
                            select med;
                MedicalRecord medrecord = query.FirstOrDefault();
                medrecord.Allergies = allergies;
                medrecord.Medication = medication;
                medrecord.Notes = notes;
                db.SaveChanges(); 

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// returns true if person exists 
        /// false otherwise 
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns></returns>
        public static bool RecordsExist(int medicalrecordid)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var count = (from p in db.MedicalRecords
                             where p.MedicalRecordID == medicalrecordid
                             select p).Count();
                if (count > 0) return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}