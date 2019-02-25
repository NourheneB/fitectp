using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Business
{
    public class CourseBL
    {
        private SchoolContext db = new SchoolContext();

        public IEnumerable<Course> GetAllCourses()
        {
            return this.db.Courses;
        }

        public IEnumerable<Course> GetNotLaunchedCourses()
        {
            List<int> launchedCourseId = db.Lessons.Where(l => l.DateStart < DateTime.Now).Select(l => l.CourseID).ToList();
            return this.db.Courses.Where(c => !launchedCourseId.Contains(c.CourseID)).OrderBy(c => c.Title);
        }

        public IEnumerable<Course> GetLaunchedCourses()
        {
            List<int> launchedCourseId = db.Lessons.Where(l => l.DateStart < DateTime.Now).Select(l => l.CourseID).ToList();
            return this.db.Courses.Except(db.Courses.Where(c => !launchedCourseId.Contains(c.CourseID)).OrderBy(c => c.Title));
        }


    }
}