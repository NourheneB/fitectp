using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Tests.Tools
{
    public class EntityGenerator
    {

        public Student CreateStudent(string lastname, string firstname, string login, string password)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname,
                Login = login,
                Password = password,
                EnrollmentDate = DateTime.Now,
            };

            DBUtils.db.Students.Add(student);
            DBUtils.db.SaveChanges();
            return student;
        }

        public Instructor CreateInstructor(string lastname, string firstname, string login, string password)
        {
            var instructor = new Instructor()
            {
                LastName = lastname,
                FirstMidName = firstname,
                Login = login,
                Password = password,
                HireDate = DateTime.Now,
            };

            DBUtils.db.Instructors.Add(instructor);
            DBUtils.db.SaveChanges();
            return instructor;
        }
    }
}
