using ContosoUniversity.Controllers;
using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace ContosoUniversity.Tests.Controllers
{
    public class StudentControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentController controllerToTest;
        private EntityGenerator generator;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            DBUtils.db = new DAL.SchoolContext(this.ConnectionString);
            generator = new EntityGenerator();
            controllerToTest = new StudentController();
            controllerToTest.DbContext = DBUtils.db;
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
        }

        [TearDown]
        public void TearDown()
        {
            SettingUpTests();
        }

        #region Initial Tests
        [Test]
        public void GetDetails_ValidStudent_Success()
        {
            string expectedLastName = "Dubois";
            string expectedFirstName = "George";
            string login = "login";
            string password = "password";

            Student student = generator.CreateStudent(expectedLastName, expectedFirstName, login, password);

            var result = controllerToTest.Details(student.ID) as ViewResult;
            var resultModel = result.Model as Student;

            Assert.That(result, Is.Not.Null);
            Assert.That(resultModel, Is.Not.Null);
            Assert.That(expectedLastName, Is.EqualTo(resultModel.LastName));
            Assert.That(expectedFirstName, Is.EqualTo(resultModel.FirstMidName));
        }

        [Test]
        public void GetDetails_InvalidStudent_Fail404()
        {
            const int expectedStatusCode = 404;
            const int invalidId = 99999999;

            var result = controllerToTest.Details(invalidId) as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(expectedStatusCode, Is.EqualTo(result.StatusCode));
        }

        [Test]
        public void Edit_ValidStudentData_Success()
        {
            string expectedLastName = "Wood";
            string previousLastName = "Dubois";
            string previousFirstName = "George";
            string login = "login";
            string password = "password";

            Student student = generator.CreateStudent(previousLastName, previousFirstName, login, password);
            student.LastName = expectedLastName;

            FormDataHelper.PopulateFormData(controllerToTest, student);

            var result = controllerToTest.EditPost(student.ID) as ViewResult;

            Student savedStudent = controllerToTest.DbContext.Students.Find(student.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That((result.Model as Student).LastName, Is.EqualTo(expectedLastName));
            Assert.That(savedStudent.LastName, Is.EqualTo(expectedLastName));
        }
        #endregion

        #region SubscribeToAnEnrollment Tests

        //Arrange for all tests
        string testlastname = "testlastname";
        string testfirstname = "testfirstname";
        string testlogin = "testlogin";
        string testpassword = "testpassword";
        string testcourse = "testcourse";

        [Test]
        public void CourseExists_CourseExists_True()
        {
            Course course = generator.CreateCourse(testcourse);
            var result = StudentEnrollmentBL.CourseExists(course.CourseID);
            Assert.True(result);
        }

        [Test]
        public void CourseExists_CourseDoesntExist_False()
        {
            var result = StudentEnrollmentBL.CourseExists(-2);
            Assert.False(result);
        }

        [Test]
        public void CanStudentSubscribe_Can_True()
        {
            Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
            Course course = generator.CreateCourse(testcourse);
            var result = StudentEnrollmentBL.CanStudentSubscribe(student.ID,course.CourseID);
            Assert.True(result);
        }

        [Test]
        public void CanStudentSubscribe_Cant_False()
        {
            Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
            Course course = generator.CreateCourse(testcourse);
            Enrollment enrollment = generator.CreateEnrollment(student, course);
            var result = StudentEnrollmentBL.CanStudentSubscribe(student.ID,course.CourseID);
            Assert.False(result);
        }

        [Test]
        public void Subscribe_SubscribeStudent_StudentAdded()
        {
            Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
            Course course = generator.CreateCourse(testcourse);
            StudentEnrollmentBL.Subscribe(student.ID, course.CourseID);
            var enrollmentexist = DBUtils.db.Enrollments.FirstOrDefault(e => e.StudentID == student.ID && e.CourseID == course.CourseID);
            Assert.NotNull(enrollmentexist);
        }

        [Test]
        public void Subscribe_StudentNotAlreadySubscribed_StudentCanSubribe()
        {
            //Arrange
            Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
            Course course = generator.CreateCourse(testcourse);
            Mock<StudentEnrollmentBL> mockedService = new Mock<StudentEnrollmentBL>();
            mockedService.Setup(m => m.CourseExists(course.CourseID)).Return(true);

            //Act & Assert
            this.controllerToTest.Subscribe = new controllerToTest.Subscribe(this.mockedService.Object);

            var result = controllerToTest.Subscribe(course.CourseID) as RedirectToRouteResult;
            //TempDataNull
            Assert.AreEqual("Details", result.RouteValues["action"]);
            Assert.AreEqual("Student", result.RouteValues["controller"]);
        }

        //[Test]
        //public void Subscribe_StudentAlreadySubscribed_Error()
        //{
        //    //Arrange
        //    Course course = generator.CreateCourse(testcourse);
        //    Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
        //    Enrollment enrollment = generator.CreateEnrollment(student, course);

        //    //Act & Assert
        //    enrollment = controllerToTest.DbContext.Enrollments.FirstOrDefault(e => e.StudentID == student.ID && e.CourseID == course.CourseID);
        //    var result = controllerToTest.Subscribe(course.CourseID);

        //    Assert.That(enrollment, Is.Not.Null);
        //    result = result as RedirectToRouteResult;

        //    //Assert.AreEqual("Details", result.RouteValues["action"]);
        //    //Assert.AreEqual("Student", result.RouteValues["controller"]);
        //}

        //[Test]
        //public void Subscribe_CourseDoesntExist_Error()
        //{
        //    //Arrange
        //    Student student = generator.CreateStudent(testlastname, testfirstname, testlogin, testpassword);
        //    //Act
        //    var result = controllerToTest.Subscribe(-2);
        //}
        #endregion
    }
}
