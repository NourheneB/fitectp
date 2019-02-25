using ContosoUniversity.DTOModels;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Services
{
    public class TransformToDTO
    {
        /// <summary>
        /// This static void transform a student to a studentDTO for the ApiService
        /// </summary>
        /// <param name="student"></param>
        /// <returns>studentDTO</returns>
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

        /// <summary>
        /// This static void transform an instructor to an instructorDTO for the ApiService
        /// </summary>
        /// <param name="instructor"></param>
        /// <returns>instructorDTO</returns>
        public static InstructorDTO TransformInstructorToInstructorDTO(Instructor instructor)
        {
            InstructorDTO instructorDTO = new InstructorDTO();


            List<LessonDTO> lessonsDTO = new List<LessonDTO>();

            if (instructor.Lessons!= null)
            {
                foreach (Lesson lesson in instructor.Lessons)
                {
                    LessonDTO lessonDTO = new LessonDTO();
                    lessonDTO.courseId = lesson.CourseID;
                    lessonDTO.day = lesson.Day.ToString().ToLower();
                    lessonDTO.startHour = lesson.StartHour.ToString("HH'h'mm");
                    lessonDTO.duration = lesson.EndHour.Subtract(lesson.StartHour).TotalMinutes.ToString() ;
                    lessonsDTO.Add(lessonDTO);
                }
            }

            instructorDTO.instructorId = instructor.ID;
            instructorDTO.schedule = lessonsDTO;

            return instructorDTO;
        }
    }
}