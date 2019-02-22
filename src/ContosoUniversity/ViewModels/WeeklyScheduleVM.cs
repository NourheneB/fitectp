using ContosoUniversity.Enumeration;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class WeeklyScheduleVM
    {
        public string UserName { get; set; }
        public Dictionary<int, Dictionary<CourseDay, string>> Agenda { get; set; }
    }
}