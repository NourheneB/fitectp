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
                Enrollments = new List<Enrollment>()
            };

            DBUtils.db.Students.Add(student);
            DBUtils.db.SaveChanges();
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

        public Course CreateCourse(string title, string name, string lastname, string firstname, string login, string password)
        {
            EntityGenerator generator = new EntityGenerator();
            Course course = new Course()
            {
                Title = title,
                Department = generator.CreateDepartment(name, lastname, firstname, login, password),
                
            };
            DBUtils.db.Courses.Add(course);
            DBUtils.db.SaveChanges();
            
            return course;
        }

        public Enrollment CreateEnrollment(Student student, Course course)
        {
            var enrollment = new Enrollment()
            {
                CourseID=course.CourseID,
                StudentID=student.ID
            };
            DBUtils.db.Enrollments.Add(enrollment);
            DBUtils.db.SaveChanges();

            return enrollment;
        }

        public Department CreateDepartment(string name, string lastname, string firstname, string login, string password)
        {
            EntityGenerator generator = new EntityGenerator();
            Department department = new Department()
            {
                Name=name,
                Budget=1000,
                StartDate=DateTime.Now,
                Administrator=generator.CreateInstructor(lastname, firstname, login,  password)

            };
            DBUtils.db.Departments.Add(department);
            DBUtils.db.SaveChanges();

            return department;
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
