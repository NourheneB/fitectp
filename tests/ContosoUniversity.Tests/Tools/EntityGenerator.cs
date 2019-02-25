using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.ViewModels;

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
        public PersonVM CreateStudentVM(string lastname, string firstname, string login, string password)
        {
            PersonVM StudentTest = new PersonVM()
            {
                LastName = lastname,
                FirstMidName = firstname,
                Login = login,
                Password = password,
                ConfirmPassword = password,
                Role="Student"
            };
            return StudentTest;
        }


        public PersonVM CreateInstructorVM(string lastname, string firstname, string login, string password)
        {
            PersonVM InstructorTest = new PersonVM()
            {
                LastName = lastname,
                FirstMidName = firstname,
                Login = login,
                Password = password,
                ConfirmPassword = password,
                Role = "Instructor"
            };

          
            return InstructorTest;
        }

        public Person GetLogin(string login, string password)
        {

            LoginVM PersonTest = new LoginVM()
            {
                Login = login,
                Password = password

            };
            DBUtils.db.People.Add(PersonTest);
            DBUtils.db.SaveChanges();
            return PersonTest;
        }


    }
}
