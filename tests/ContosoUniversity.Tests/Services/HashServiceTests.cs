using NUnit.Framework;
using System;
using ContosoUniversity.Services;
using ContosoUniversity.Tests.Tools;

namespace ContosoUniversity.Tests.Services
{
    class HashServiceTests : IntegrationTestsBase
    {

        [Test]
        public void GenerateSHA256String_stringToCompare_True()
        {

            string inputString = "password123";
            string hash = HashService.GenerateSHA256String(inputString);
            string stringToCompare = HashService.GenerateSHA256String("password123");
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            int result = comparer.Compare(stringToCompare, hash);

            Assert.AreEqual(0, result);

        }
    }
}