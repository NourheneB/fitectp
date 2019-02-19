using ContosoUniversity.Buisiness;
using ContosoUniversity.DTOModels;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContosoUniversity.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentApiController : ApiController
    {
        [Route("{id}")]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // get the student with the id
            StudentBL sbl = new StudentBL();
            Student studentToTransform = sbl.GetStudentById(id);

            if ( sbl == null)
            {
                return NotFound();
            }

            else
            {
                 return Ok(TransformStudentDTO.TransformStudentToStudentDTO(studentToTransform));
            }
        }

     
    }
}