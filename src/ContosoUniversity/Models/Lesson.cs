using ContosoUniversity.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Lesson
    {
        [Required]
        public int LessonID { get; set; }

        [Required]
        public int InstructorID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartHour { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndHour { get; set; }

        //TODO : change the type and the calcul (see TransformToDTO service)
        public decimal Duration
        {
            get
            {
                return (EndHour.Hour - StartHour.Hour);
            }
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }

        [Required]
        public CourseDay Day { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual Course Course { get; set; }
    }
}