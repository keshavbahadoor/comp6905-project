using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Healthee.DataAbstraction;
using Healthee.EFModels;
using Healthee.Models;
using Healthee.DataModels;
using Newtonsoft.Json; 


namespace HealtheeTestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting tests");
            DataService.DEBUG = true;
            try
            {

                int id;
                int.TryParse("n", out id); 
                Console.WriteLine(id); 

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! : " + e.Message); 
                Console.WriteLine(e.StackTrace); 
            }

            Console.ReadLine(); 
        }

        public static string toJSON(Object o)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(o, new JsonSerializerSettings()
                                                                        {
                                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                                                                        });
            return (string)json; 
        }

        private static void TestDB()
        {
            Console.WriteLine("Test DB Exists : ");
            HealtheeEntities db = new HealtheeEntities();
            if (db.Database.Exists()) Console.WriteLine("PASS");
            else Console.WriteLine("FAIL");
            Console.WriteLine(); 
        }
        private static void TestPatientSearch()
        {
            // Testing person search 
            List<PatientData> pData = DataService.GetPatients("Cara", null, null, null);
            Console.WriteLine(toJSON(pData));
        }
    }
}
