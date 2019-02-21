using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Tests.Tools
{
    public class EntityGenerator
    {
        private readonly SchoolContext dbContext;

        public EntityGenerator(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Student CreateStudent(string lastname, string firstname)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname
            };

            this.dbContext.Students.Add(student);
            return student;
        }

        //CreatePerson for LoginPersonTest
        public Student CreatePersonWithLoginAndPassword(LoginVM model)
        {
            var student = new Student()
            {
                LastName = "lastname",
                FirstMidName = "firstname",
                Login = model.Login,
                Password = model.Password,
                EnrollmentDate = DateTime.Now
            };

            this.dbContext.Students.Add(student);
            dbContext.SaveChanges();
            return student;
        }
    }
}
