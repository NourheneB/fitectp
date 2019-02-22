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
using ContosoUniversity.Services;

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

            //Assert

            Assert.IsNotNull(dbContext.Students.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == passwordTest));
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
            Instructor instructor = dbContext.Instructors.SingleOrDefault(s => s.LastName == "Green" && s.FirstMidName == "Harry" && s.Login == "login1" && s.Password == passwordTest);
            //Assert

            Assert.IsNotNull(instructor);
        }
        #endregion

        #region LoginPerson Tests
        //Valitation peut se faire peut-etre cote client?
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
            
            EntityGenerator generator = new EntityGenerator(dbContext);
            generator.CreatePersonWithLoginAndPassword(modelTest);

            //Act
            var user = authenticationBusinessToTest.LoginPerson(modelTest);
            //Assert
            Assert.IsNotNull(user);
        }

        #endregion

    }
}
