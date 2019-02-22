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


    }
}