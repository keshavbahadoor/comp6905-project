using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataModels;
using System.Text;
using System.Security.Cryptography;

namespace Healthee.DataAbstraction
{
    public class Authentication
    { 
        /// <summary>
        /// authenticates doctor login 
        /// creates activity for successful login 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginData DoctorAuth(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new LoginData() {  loggedin = false };
            }
            // TODO : password encryption 
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                var query = (from d in db.Doctors
                             where d.Username == username
                             &&  d.Password == password
                             select d);
                if (query.Count() > 0)
                {
                    Doctor d = query.FirstOrDefault();
                    UserActivity.AddDoctorActivity((int)ActivityEnum.DoctorLogin, d.DoctorID, (int)StatusEnum.Success, "Logged In", username);
                    return new LoginData() { doctorid = d.DoctorID, loggedin = true };
                }
                else
                {
                    return new LoginData() { loggedin = false };
                }
                
            }
            catch (Exception ex)
            {
                return new LoginData() { loggedin = false };
            }
        }

        /// <summary>
        /// authenticates doctor login 
        /// creates activity for successful login 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginData PatientAuth(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new LoginData() { loggedin = false };
            }
            // TODO : password encryption 
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                var query = (from p in db.Patients
                             where p.Username == username
                             && p.Password == password
                             select p);
                if (query.Count() > 0)
                {
                    Patient p = query.FirstOrDefault(); 
                    return new LoginData() { patientid = p.PatientID, loggedin = true };
                }
                else
                {
                    return new LoginData() { loggedin = false };
                }

            }
            catch (Exception ex)
            {
                return new LoginData() { loggedin = false };
            }
        }
        /// <summary>
        /// Encrypts a given password and returns the encrypted data
        /// as a base64 string.
        /// </summary>
        public static string Encrypt(string plainText)
        {
            // TODO : Replace with proper hashing and salt
            return plainText ; 
        }
        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        public static string Decrypt(string cipher)
        {
            // TODO : Replace 
            return cipher; 
        }
    }
}