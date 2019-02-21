using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DTOModels
{
    public class StudentDTO
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string enrollmentDate { get; set; }
        public List<EnrollmentDTO> enrollments { get; set; }
    }
}