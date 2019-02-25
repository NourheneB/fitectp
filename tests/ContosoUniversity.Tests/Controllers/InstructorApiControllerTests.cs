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
    public class InstructorApiControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private InstructorApiController controllerToTest;
        private InstructorBL instructorBL;
        private EntityGenerator generator;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            DBUtils.db = new DAL.SchoolContext(this.ConnectionString);
            instructorBL = new InstructorBL();
            generator = new EntityGenerator();
            controllerToTest = new InstructorApiController();
        }
        [TearDown]
        public void TearDown()
        {
            SettingUpTests();
        }

        //Tests for GetInstructorById()
        [Test]
        public void GetInstructorById_InstructorIdExists_ReturnInstructor()
        {
            //Arrange
            Instructor instructor = generator.CreateInstructor("John", "Doe", "Doe", "Doe");
            //Act
            var result = instructorBL.GetInstructorById(instructor.ID);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.ID, instructor.ID);
        }
        [Test]
        public void GetInstructorById_InstructorIdDoesntExist_Null()
        {
            //Act
            var result = instructorBL.GetInstructorById(0);
            //Assert
            Assert.IsNull(result);
        }

        //Tests for TransformInstructorToInstructorDTO()
        [Test]
        public void TransformInstructorToInstructorDTO_InstructorIdExists_ReturnInstructor()
        {
            //Arrange
            Instructor instructor = generator.CreateInstructor("John", "Doe", "Doe", "Doe");
            //Act
            var result = TransformToDTO.TransformInstructorToInstructorDTO(instructor);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf(typeof(InstructorDTO), result);
            Assert.AreEqual(result.instructorId, instructor.ID);
        }

        //Tests for InstructorApiController()
        [Test]
        public void InstructorApiController_InstructorIdExists_ReturnOK()
        {
            //Arrange
            Instructor instructor = generator.CreateInstructor("John", "Doe", "Doe", "Doe");
            IHttpActionResult result = controllerToTest.Get(instructor.ID);
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<InstructorDTO>), result);

            OkNegotiatedContentResult<InstructorDTO> instructor_result = result as OkNegotiatedContentResult<InstructorDTO>;
            Assert.AreEqual(instructor_result.Content.instructorId, instructor.ID);

        }
        [Test]
        public void InstructorApiController_InstructorIdDoesntExist_Return404()
        {
            //Arrange
            Instructor instructor = generator.CreateInstructor("John", "Doe", "Doe", "Doe");
            IHttpActionResult result = controllerToTest.Get(-2);
            //Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), result);
        }
    }
}
