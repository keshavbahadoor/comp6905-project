using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataModels;

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
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                var query = (from d in db.Doctors
                             where d.Username == username
                             && d.Password == password
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
    }
}