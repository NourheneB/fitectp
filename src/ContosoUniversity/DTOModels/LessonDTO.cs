using ContosoUniversity.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DTOModels
{
    public class LessonDTO
    {
        public int courseID { get; set; }
        public CourseDay day { get; set; }
        public DateTime startHour { get; set; }
        public TimeSpan duration { get; set; }

    }
}