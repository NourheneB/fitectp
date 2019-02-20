using ContosoUniversity.Controllers;
using ContosoUniversity.Business;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using System.Data.Entity;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContosoUniversity.ViewModels;
using System.Linq;

namespace ContosoUniversity.Tests.Business
{
   
    public class AuthenticationBusinessTests : IntegrationTestsBase
    {
        //private MockHttpContextWrapper httpContext;
        private AuthenticationBusiness authenticationBusinessToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
           // httpContext = new MockHttpContextWrapper();
            authenticationBusinessToTest = new AuthenticationBusiness();
           // authenticationBusinessToTest.BusinessContext = new ControllerContext(httpContext.Context.Object, new RouteData(), authenticationBusinessToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            authenticationBusinessToTest.DbContext = dbContext;
        }
        [TearDown]
        public void CleanUp()
        {
            SettingUpTests();
            //var studentToRemove = dbContext.Students.FirstOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == "password1");
            //dbContext.Students.Remove(studentToRemove);
        }

        #region CreateUser Tests

        //CreateNewStudent Method
        [Test]
        public void CreateNewStudent_CreateNewStudent_NewStudentCreated()
        {
            //Arrange

            PersonVM modelTest = new PersonVM()
            {
                LastName = "Green",
                FirstMidName = "Harry",
                Login = "login1",
                Password = "password1",
                ComfirmPassword = "password1",
                //on attendant le role : student
            };

            //Act
           authenticationBusinessToTest.CreateNewStudent(modelTest);

            //Assert

            Assert.IsNotNull(dbContext.Students.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == "password1"));
        }

        

        //CreateNewInstructor Method
        [Test]
        public void CreateNewInstructor_CreateNewInstructor_NewInstructorCreated()
        {
            //Arrange

            PersonVM modelTest = new PersonVM()
            {
                LastName = "Green",
                FirstMidName = "Harry",
                Login = "login1",
                Password = "password1",
                ComfirmPassword = "password1",
                //on attendant le role : instructor
            };

            //Act
            authenticationBusinessToTest.CreateNewInstructor(modelTest);

            //Assert

            Assert.IsNotNull(dbContext.Instructors.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == "password1"));
        }

      /*  [TearDown]
        public void CleanUp()
        {
            SettingUpTests();
            //var instructorToRemove = dbContext.Students.FirstOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == "password1");
            //dbContext.Instructors.Remove(instructorToRemove);
        }*/




        /*


        #endregion

        #region Authenticate Tests

        //Arrange for the following authentication tests
        string expectedlogin = "login";
        string expectedpassword = "password";

        [SetUp] //to initialize a user to authentification test
        public void CreationOfUserToTest()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Student student = generator.CreateStudent(expectedlogin, expectedpassword);
        }

        [TearDown] //to clean db after each test
        public void CleanUpDb()
        {
            SettingUpTests();
        }

       /* [TestMethod]
        public void CreateNewStudent_EtatInitial_EtatAttendu()
        {
            throw new NotImplementedException();
        }*/
        #endregion
    }
}
