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
                Enrollments = new List<Enrollment>()
            };

            DBUtils.db.Students.Add(student);
            DBUtils.db.SaveChanges();
            return student;
        }

        public Course CreateCourse(string title)
        {
            Course course = new Course()
            {
                Title = title,
                Department = new Department(),
                
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

        public Department CreateDepartment(string name)
        {
           Department department = new Department()
            {
                Name=name,
                Budget=1000,
                StartDate=DateTime.Now,
                Administrator=new Instructor()

            };
            DBUtils.db.Departments.Add(department);
            DBUtils.db.SaveChanges();

            return department;
        }
    }
}
