using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class StudentApiStudentTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentApiController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new StudentApiController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void Repository_StudentIdExist_ReturnStudent()
        {
            //Arrange
            //Act
            var result = repository.Get()
            //Assert
            Assert.NotNull(result);
            Assert.That(result.IsInstanceOf Student); ;
        }

        [Test]
        public void Get_StudentExists_Success()

        {
            //Arrange
            string expectedjson = "{\"id\":4,\"lastname\":\"Barzdukas\",\"firstname\":\"Gytis\",\"enrollmentDate\":\"01/09/2012\",\"enrollments\":[{\"courseId\":1050},{\"courseId\":1050}";
            private Mock<repository> mocked =new Mock<repository>();
            mocked
                .SetUp(m=>mbox.repository It.Is<int>(x=>x==id))
        .Returns(student);
            //Act
            IHttpActionResult httpActionResult = controllerToTest.Get(15);
            //Assert
            Assert.That(); //typeréponse=typeOk
            Assert.AreEqual(httpActionResult, expectedjson);//réponse=chainejsonattendue
        }

        [Test]
        public void Get_StudentDoesntExist_404()
        {
            Assert.That(); //typeréponse=typeNotFound

        }
    }
}
