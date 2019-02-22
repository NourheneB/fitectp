using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using System.Web.Http;

namespace ContosoUniversity.Controllers.Api
{
    [RoutePrefix("api/students")]
    public class StudentApiController : ApiController
    {

        private StudentBL sbl = new StudentBL();
        public StudentBL Sbl { get => sbl; set => sbl = value; }

        [Route("{id}")]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // get the student with the id
            Student studentToTransform = sbl.GetStudentById(id);

            if (studentToTransform == null)
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