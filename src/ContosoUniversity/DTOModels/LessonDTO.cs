using ContosoUniversity.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DTOModels
{
    public class LessonDTO
    {
        public int courseId { get; set; }
        public string day { get; set; }
        public string startHour { get; set; }
        public string duration { get; set; }

    }
}