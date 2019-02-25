using ContosoUniversity.Business;
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
    [RoutePrefix("api/instructors")]
    public class InstructorApiController : ApiController
    {
        private InstructorBL instructorBL = new InstructorBL();
        public InstructorBL InstructorBL { get => instructorBL; set => instructorBL = value; }

        [Route("{id}")]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // get the student with the id
            Instructor instructorToTransform = instructorBL.GetInstructorById(id);

            if (instructorToTransform == null)
            {
                return NotFound();
            }

            else
            {
                InstructorDTO instructorToReturn = TransformToDTO.TransformInstructorToInstructorDTO(instructorToTransform);
                return Ok(instructorToReturn);
            }
        }
    }
}
