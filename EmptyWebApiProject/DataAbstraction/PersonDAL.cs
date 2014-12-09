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
    public class PersonDAL : AbstractDAL
    { 
       
        /// <summary>
        /// Inserts person data into database 
        /// returns last inserted id 
        /// </summary>
        /// <returns></returns>
        public static int InsertPerson(string firstname, string lastname, string gender, DateTime dob, string nationalid, string mobilenumber,
                                       string homenumber, string worknumber, string address1, string address2, string city, string country, string email)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                Person p = new Person { 
                        FirstName = firstname, 
                        LastName = lastname, 
                        Gender = gender, 
                        NationalID = nationalid,
                        MobileNumber = mobilenumber,
                        HomeNumber = homenumber,
                        WorkNumber = worknumber,
                        Address1 = address1, 
                        Address2 = address2,
                        City = city,
                        Country = country,
                        Email = email
                };

                if (dob != DateTime.MinValue) p.DateOfBirth = dob; 

                db.People.Add( p);
                db.SaveChanges();
                return p.PersonID;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// Updates a person record 
        /// </summary>
        /// <param name="personid"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="dob"></param>
        /// <param name="nationalid"></param>
        /// <param name="mobilenumber"></param>
        /// <param name="homenumber"></param>
        /// <param name="worknumber"></param>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static bool UpdatePerson(int personid, string firstname, string lastname, string gender, DateTime dob, string nationalid, string mobilenumber,
                                       string homenumber, string worknumber, string address1, string address2, string city, string country, string email)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var query = from p in db.People
                            where p.PersonID == personid
                            select p;
                Person person = query.FirstOrDefault(); 
                //if (person == null) return false;

                person.FirstName = firstname;
                person.LastName = lastname;
                person.Gender = gender;
                if (dob != DateTime.MinValue) person.DateOfBirth = dob;
                person.NationalID = nationalid;
                person.MobileNumber = mobilenumber;
                person.HomeNumber = homenumber;
                person.Address1 = address1;
                person.Address2 = address2;
                person.City = city;
                person.Country = country;
                person.Email = email;
                person.LastUpdated = DateTime.Now;

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
        public static bool PersonExist(int personid)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;

                var count = (from p in db.People
                             where p.PersonID == personid
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