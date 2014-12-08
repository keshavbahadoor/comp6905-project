using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataAbstraction;
using Healthee.DataModels;
using Newtonsoft.Json.Linq; 

namespace Healthee.Models
{
    /// <summary>
    /// Activity type enumeration 
    /// </summary>
    public enum ActivityEnum
    {
        None = 0, 
        DoctorRegistration = 1,
        PatientRegistratoin = 2, 
        PatientSearch = 3,
        UpdateProfile = 4,
        DoctorLogin = 5,
        CreateAppointment = 5,
        ViewAppointments = 6
    }
    /// <summary>
    /// Status enumeration 
    /// </summary>
    public enum StatusEnum
    {
        None = 0, 
        Success = 1, 
        Failure = 2, 
        Pending = 3
    }
    /// <summary>
    /// Logs both patent user activity and doctor user activity 
    /// Doctor user activity will be used for billing and audit trails 
    /// </summary>
    public class UserActivity
    {
        public static bool AddDoctorActivity(int activityType, int doctorID, int statusID, string details, string data)
        {
            if (doctorID < 0 || activityType < 0 || statusID < 0) 
                return false;

            DoctorDAL.AddDoctorActivity(activityType, doctorID, statusID, details, data);
            return true;
        }

    }
}