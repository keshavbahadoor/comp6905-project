using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Healthee.EFModels;
using Healthee.DataModels;
using Healthee.Logging;
using System.Diagnostics;

namespace Healthee.DataAbstraction
{
    public class AppointmentDAL : AbstractDAL
    {
        /// <summary>
        /// Adds new appointment 
        /// </summary>
        /// <returns></returns>
        public static int InsertAppointment(int doctorid, int patientid, int statusid, string date, string time, string notes)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                Appointment a = new Appointment
                { 
                     DoctorID = doctorid,
                     PatientID = patientid,
                     StatusID = statusid,
                     AppointmentDate = date,
                     AppointmentTime= time,
                     Notes = notes 
                };

                db.Appointments.Add(a); 
                db.SaveChanges();
                return a.AppointmentID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// <summary>
        /// returns appointments for a doctor 
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns></returns>
        public static List<Appointment> GetDoctorAppointments(int doctorid)
        { 
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                //db.Database.Log = msg => Trace.WriteLine(msg);
                var query = from a in db.Appointments
                            where a.DoctorID == doctorid
                            select a;

                return query.ToList<Appointment>(); 
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// returns appointments for patients 
        /// </summary>
        /// <param name="patientid"></param>
        /// <returns></returns>
        public static List<Appointment> GetPatientAppointments(int patientid)
        {
            try
            {
                HealtheeEntities db = new HealtheeEntities();
                if (DEBUG) db.Database.Log = Console.WriteLine;
                var query = from a in db.Appointments
                            where a.PatientID == patientid
                            select a;

                return query.ToList<Appointment>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}