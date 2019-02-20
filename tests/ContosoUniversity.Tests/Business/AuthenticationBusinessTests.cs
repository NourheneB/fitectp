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
using System;

namespace ContosoUniversity.Tests.Business
{
    [TestClass]
    class AuthenticationBusinessTests : IntegrationTestsBase
    {
        [TestMethod]
        public void CreateNewStudent_EtatInitial_EtatAttendu()
        {
            throw new NotImplementedException();
        }
    }
}
