using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class StudentDetailsData
    {
        public int ID { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public Dictionary<int, string> Enrollments { get; set; }

        public Dictionary<int, string> Courses { get; set; }
    }
}