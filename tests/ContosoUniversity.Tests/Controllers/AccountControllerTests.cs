using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class AccountControllerTests : IntegrationTestsBase
    {
        //private MockHttpContextWrapper httpContext;
        private AccountController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
           // httpContext = new MockHttpContextWrapper();
            controllerToTest = new AccountController();
           // controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }
        [TearDown]
        public void CleanUp()
        {
            SettingUpTests();
        }
       
    }
}
