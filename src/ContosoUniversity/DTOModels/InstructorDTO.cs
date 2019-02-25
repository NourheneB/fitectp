using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DTOModels
{
    public class InstructorDTO
    {
        public int instructorId { get; set; }
        public List<LessonDTO> schedule { get; set; }
    }
}