using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Business;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.Services;

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
                Password = HashService.GenerateSHA256String(model.Password),
                EnrollmentDate = DateTime.Now
            };

            this.dbContext.Students.Add(student);
            dbContext.SaveChanges();
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
