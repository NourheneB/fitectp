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

        public Course CreateCourse(string title)
        {
            Course course = new Course()
            {
                Title = title,
                DepartmentID = 1000,
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
    }
}
