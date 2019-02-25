using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Business
{
    public class StudentEnrollmentBL
    {
        public static bool CourseExists(int courseID)
        {
            Course courseExists = DBUtils.db.Courses.FirstOrDefault(c => c.CourseID == courseID);
            if (courseExists != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanStudentSubscribe(int studentID, int id)
        {
            Enrollment enrollmentExists = DBUtils.db.Enrollments.FirstOrDefault(e => e.CourseID == id && e.StudentID == studentID);
            if (enrollmentExists == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Subscribe(int studentID, int id)
        {
            DBUtils.db.Enrollments.Add(new Enrollment { CourseID = id, StudentID = studentID });
            DBUtils.db.SaveChanges();
        }
    }
}