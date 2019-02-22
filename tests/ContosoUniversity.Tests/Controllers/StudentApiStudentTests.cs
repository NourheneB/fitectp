using ContosoUniversity.Business;
using ContosoUniversity.Controllers.Api;
using ContosoUniversity.DTOModels;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using ContosoUniversity.Tests.Tools;
using NUnit.Framework;
using System.Web.Http;
using System.Web.Http.Results;

namespace ContosoUniversity.Tests.Controllers
{
    public class StudentApiStudentTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentApiController controllerToTest;
        private StudentBL studentBL;
        private EntityGenerator generator;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            DBUtils.db = new DAL.SchoolContext(this.ConnectionString);
            studentBL = new StudentBL();
            generator = new EntityGenerator();
            controllerToTest = new StudentApiController();
        }
        [TearDown]
        public void TearDown()
        {
            SettingUpTests();
        }

        //Tests for GetStudentById()
        [Test]
        public void GetStudentById_StudentIdExists_ReturnStudent()
        {
            //Arrange
            Student student = generator.CreateStudent("John","Doe","Doe","Doe");
            //Act
            var result = studentBL.GetStudentById(student.ID);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.ID, student.ID);
        }
        [Test]
        public void GetStudentById_StudentIdDoesntExist_Null()
        {
             //Act
            var result = studentBL.GetStudentById(0);
            //Assert
            Assert.IsNull(result);
        }

        //Tests for TransformStudentToStudentDTO()
        [Test]
        public void TransformStudentToStudentDTO_StudentIdExists_ReturnStudent()
        {
            //Arrange
            Student student = generator.CreateStudent("John", "Doe", "Doe", "Doe");
            //Act
            var result = TransformStudentDTO.TransformStudentToStudentDTO(student);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf(typeof(StudentDTO),result);
            Assert.AreEqual(result.id, student.ID);
        }

        //Tests for StudentApiController()
        [Test]
        public void StudentApiController_StudentIdExists_ReturnJSON()
        {
            //Arrange
            Student student = generator.CreateStudent("John", "Doe", "Doe", "Doe");
            //string expectedjson ="{\"id\":"+student.ID+",\"lastname\":\""+student.LastName+"\",\"firstname\":\""+student.FirstMidName+"\",\"enrollmentDate\":\""+student.EnrollmentDate+",\"enrollments\":[{\"courseId\":"+student.Enrollments+"},]}";
            IHttpActionResult result = controllerToTest.Get(student.ID);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<StudentDTO>), result);

            OkNegotiatedContentResult<StudentDTO> student_result = result as OkNegotiatedContentResult<StudentDTO>;
            Assert.AreSame(student_result.Content.firstname, student.FirstMidName);
            Assert.AreSame(student_result.Content.lastname, student.LastName);
            //Assert.AreEqual(expectedjson, result);
        }
        [Test]
        public void StudentApiController_StudentIdDoesntExist_Return404()
        {
            //Arrange
            Student student = generator.CreateStudent("John", "Doe", "Doe", "Doe");
            //string expectedjson = "{\"id\":" + student.ID + ",\"lastname\":\"" + student.LastName + "\",\"firstname\":\"" + student.FirstMidName + "\",\"enrollmentDate\":\"" + student.EnrollmentDate + ",\"enrollments\":[{\"courseId\":" + student.Enrollments + "},]}";
            IHttpActionResult result = controllerToTest.Get(-2);
            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
            //Assert.AreNotEqual(expectedjson, result);
        }

    }
}
