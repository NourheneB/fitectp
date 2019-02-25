using ContosoUniversity.Business;
using ContosoUniversity.Controllers;
using ContosoUniversity.Controllers.Api;
using ContosoUniversity.DAL;
using ContosoUniversity.DTOModels;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using ContosoUniversity.Tests.Tools;
using ContosoUniversity.ViewModels;
using NUnit.Framework;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    class AccountControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private AccountController controllerToTest;
        private SchoolContext dbContext;
        EntityGenerator generator = new EntityGenerator();

        //#region Arrange
        //private readonly LoginVM LoginInstructor = new LoginVM()
        //{

        //    Login = "nourhene",
        //    Password = "123",

        //};

        //private readonly LoginVM LoginStudent = new LoginVM()
        //{

        //    Login = "stanmatina",
        //    Password = "123",

        //};
        //private readonly PersonVM RegisterStudent = new PersonVM()
        //{
        //    FirstMidName = "Matoula",
        //    LastName = "StANMATINA",
        //    Login = "stanmatina",
        //    Password = "123",
        //    ConfirmPassword = "123",
        //    Role = "Instructor"

        //};



        //private readonly PersonVM RegisterInstructor = new PersonVM()
        //{
        //    FirstMidName = "Nourhene",
        //    LastName = "BAOUAB",
        //    Login = "nourhene",
        //    Password = "123",
        //    ConfirmPassword = "123",
        //    Role = "Student"
        //};
        //#endregion
        string firstname = "Nourhene";
        string lastname = "BAOUAB";
        string login = "nourhene";
        string password = "123";

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new AccountController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }
        [TearDown]
        public void TearDown()
        {
            SettingUpTests();
        }
        [Test]
        public void RegisterStudent_Student_ViewLogin()
        {
            PersonVM modelTest = generator.CreateStudentVM(lastname, firstname, login, password);
            var result = controllerToTest.Register(modelTest);
            //Student studentcreated = DBUtils.db.Students.FirstOrDefault(s=>s.ID==result.Content.Id);
            RedirectToRouteResult resultat = result as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(resultat.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void RegisterInstructor_Instructor_ViewLogin()
        {
            PersonVM modelTest = generator.CreateInstructorVM(lastname, firstname, login, password);
            var result = controllerToTest.Register(modelTest);
            RedirectToRouteResult resultat = result as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void LoginStudent_Student_View_IndexStudent()
        {
            LoginVM personTest = new LoginVM
            {

                Login = "nourhene",
                Password = "123",

            };
            RedirectToRouteResult result = controllerToTest.Login(LoginStudent) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
        [Test]
        public void LoginInstructor_Instructor_View_IndexInstructor()
        {
            LoginVM personTest = new LoginVM
            {

                Login = "stanmatina",
                Password = "123",

            };
            Instructor instructor = generator.CreateInstructorVM
            RedirectToRouteResult result = controllerToTest.Login(LoginInstructor) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

    }
}
