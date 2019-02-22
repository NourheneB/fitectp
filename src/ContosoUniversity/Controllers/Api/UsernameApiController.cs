using ContosoUniversity.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContosoUniversity.Controllers.Api
{
    [RoutePrefix("api/userInfo")]
    public class UsernameApiController : ApiController
    {
        [Route("{username}")]
        [HttpGet]
        public IHttpActionResult Get(string username)
        {
            UsernameValidation userV = new UsernameValidation();

            bool available = userV.UsernameIsAvailable(username);

            if (available == true)
            {
                return Ok(true);
            }

            else
            {
                return Ok(false);
            }
        }
    }
}