using ContosoUniversity.DTOModels;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Services
{
    public class TransformStudentDTO
    {
        public static StudentDTO TransformStudentToStudentDTO(Student student)
        {
            StudentDTO studentDto = new StudentDTO();


            List<EnrollmentDTO> coursesList = new List<EnrollmentDTO>();

            if (student.Enrollments != null)
            {
                foreach (Enrollment enrollment in student.Enrollments)
                {
                    EnrollmentDTO course = new EnrollmentDTO();
                    course.courseId = enrollment.CourseID;
                    coursesList.Add(course);
                }
            }
            
            studentDto.id = student.ID;
            studentDto.firstname = student.FirstMidName;
            studentDto.lastname = student.LastName;
            studentDto.enrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd");
            studentDto.enrollments = coursesList;

            return studentDto;
        }
    }
}