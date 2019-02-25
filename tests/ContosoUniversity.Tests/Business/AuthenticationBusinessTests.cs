using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using NUnit.Framework;
using ContosoUniversity.ViewModels;
using System.Linq;
using ContosoUniversity.Services;

namespace ContosoUniversity.Tests.Business
{

    public class AuthenticationBusinessTests : IntegrationTestsBase
    {

        //private MockHttpContextWrapper httpContext;
        private AuthenticationBusiness authenticationBusinessToTest;
        EntityGenerator generator = new EntityGenerator();

        [SetUp]
        public void Initialize()
        {
            // httpContext = new MockHttpContextWrapper();
            //authenticationBusinessToTest.ControllerContext = new ControllerContext(HttpContent.Context.Object, new RouteData(), authenticationBusinessToTest);
            DBUtils.db = new DAL.SchoolContext(this.ConnectionString);
            authenticationBusinessToTest = new AuthenticationBusiness();
            authenticationBusinessToTest.DbContext = DBUtils.db;
        }
        [TearDown]
        public void CleanUp()
        {
            SettingUpTests();
        }

        #region CreateUser Tests

        //CreateNewStudent Method
        [Test]
        public void CreateNewStudent_NewStudent_NewStudentCreated()
        {
            //Arrange

            PersonVM modelTest = new PersonVM()
            {
                LastName = "Green",
                FirstMidName = "Harry",
                Login = "login1",
                Password = "password1",
                ConfirmPassword = "password1",
                Role = "student"

            };
            //Ceci rend le test dependant de la methode GenerateSHA256String(string inputString) de la Classe HashService
            string passwordTest = HashService.GenerateSHA256String(modelTest.Password);
            //Act
            authenticationBusinessToTest.CreateNewStudent(modelTest);
            Student student = DBUtils.db.Students.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == passwordTest);
            //Assert

            Assert.IsNotNull(student);
        }



        //CreateNewInstructor Method
        [Test]
        public void CreateNewInstructor_NewInstructor_NewInstructorCreated()
        {
            //Arrange

            PersonVM modelTest = new PersonVM()
            {
                LastName = "Green",
                FirstMidName = "Harry",
                Login = "login1",
                Password = "password1",
                ConfirmPassword = "password1",
                Role = "instructor"
            };

            //Ceci rend le test dependant de la methode GenerateSHA256String(string inputString) de la Classe HashService
            string passwordTest = HashService.GenerateSHA256String(modelTest.Password);
            //Act
            authenticationBusinessToTest.CreateNewInstructor(modelTest);
            Instructor instructor = DBUtils.db.Instructors.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == passwordTest);
            //Assert

            Assert.IsNotNull(instructor);
        }
        #endregion

        #region LoginPerson Tests
        //Validation peut se faire peut-etre cote client?
        [Test]
        public void LoginPerson_LoginPasswordFalse_UserNull()
        {
            //Arrange
            LoginVM modelTest = new LoginVM()
            {
                Login = "login1",
                Password = "password1"
            };

            //Act
            Person user = authenticationBusinessToTest.LoginPerson(modelTest);
            //Assert
            Assert.IsNull(user);
        }
        //
        [Test]
        public void LoginPerson_LoginPasswordTrue_UserNotNull()
        {
            //Arrange
            LoginVM modelTest = new LoginVM()
            {
                Login = "login1",
                Password = "password1"
            };
            
            generator.CreatePersonWithLoginAndPassword(modelTest);

            //Act
            var user = authenticationBusinessToTest.LoginPerson(modelTest);
            //Assert
            Assert.IsNotNull(user);
        }

        #endregion

    }
}
