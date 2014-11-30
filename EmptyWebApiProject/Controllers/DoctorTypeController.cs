using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataAbstraction;


namespace Healthee.Controllers
{
    public class DoctorTypeController : ApiController
    {


        public DoctorType GetDoctorTypeByID(int id)
        {

            return DataService.GetDoctorType(id); 
              
            
        }
        [HttpGet]
        public string Testd()
        {
            return "something"; 
        }

        
        public object PostPerson([FromBody] string data)
        { 

            return Request.CreateResponse(HttpStatusCode.OK, this.GetDoctorTypeByID(2));

            
        }

        

    }
}
