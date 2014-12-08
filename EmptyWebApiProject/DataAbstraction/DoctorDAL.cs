using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;
using Healthee.DataModels;

namespace Healthee.DataAbstraction
{
    /// <summary>
    /// Doctor Data Abstraction layer 
    /// </summary>
    public class DoctorDAL : AbstractDAL
    {  

        /// <summary>
        ///  GEt doctor data 
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns></returns>
        public static DoctorData GetDoctorData(int doctorid)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var query = from person in db.People
                            join doctors in db.Doctors on person.PersonID equals doctors.PersonID
                            join doctortypes in db.DoctorTypes on doctors.DoctorTypeID equals doctortypes.DoctorTypeID
                            where doctors.DoctorID == doctorid
                            where doctors.DoctorTypeID == doctortypes.DoctorTypeID
                            select new DoctorData()
                            {
                                Person = person,
                                DoctorID = doctorid,
                                DoctorType = doctortypes.Type,
                                Username = doctors.Username

                            };
                DoctorData data = query.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Ged doctor type by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// returns all doctor types 
        /// </summary>
        /// <returns></returns>
        public static List<DoctorType> GetDoctorTypes()
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                var query = from dt in db.DoctorTypes select dt;
                return query.ToList<DoctorType>();
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Inserts doctor data 
        /// returns last inserted id 
        /// </summary>
        /// <returns></returns>
        public static int InsertDoctor(int personId, int doctortypeid, string username, string password)
        {
            // perform encryption on password 
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                Doctor d = new Doctor
                {
                    PersonID = personId, 
                    DoctorTypeID = doctortypeid,
                    Username = username,
                    Password = password
                };

                db.Doctors.Add(d);
                db.SaveChanges();
                return d.DoctorID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}