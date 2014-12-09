using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Healthee.Models;
using Healthee.EFModels;
using Healthee.DataAbstraction;
using Healthee.DataModels;
using Newtonsoft.Json.Linq; 

namespace Healthee.Controllers
{
    /// <summary>
    /// handles all authentication processes 
    ///     - doctor login authentication 
    ///     - patient login authentication 
    /// </summary>
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// handles doctor authentication 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginData DoctorLogin([FromBody]JToken value)
        { 
            return Authentication.DoctorAuth((string)value.SelectToken("username"),
                                             (string)value.SelectToken("password"));

            
        }

        /// <summary>
        /// handles patient authentication 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginData PatientLogin([FromBody]JToken value)
        {
            return Authentication.PatientAuth((string)value.SelectToken("username"),
                                             (string)value.SelectToken("password"));


        }

    }
}
