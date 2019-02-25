using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Enumeration;

namespace ContosoUniversity.Business
{
    public class ScheduleBL
    {
        private SchoolContext db = new SchoolContext();

        public Dictionary<int, Dictionary<CourseDay, string>> GetWeeklyScheduleInstructor(int id)
        {
            Dictionary<int, Dictionary<CourseDay, string>> agenda = new Dictionary<int, Dictionary<CourseDay, string>>();
            for (int hour = 8; hour <= 19; hour++)
            {
                Dictionary<CourseDay, string> HourDay = new Dictionary<CourseDay, string>();

                foreach (CourseDay day in (CourseDay[])System.Enum.GetValues(typeof(CourseDay)))
                {
                    string libelle = "";

                    List<Lesson> lessonsInstructor = db.Lessons.Where(l => l.InstructorID == id).ToList();
                    Lesson lesson = db.Lessons.Where(l => (l.InstructorID == id && l.Day == day))
                        .Where(l => (l.StartHour.Hour == hour || l.StartHour.Hour < hour && l.EndHour.Hour > hour))
                        .FirstOrDefault();
                    if (lesson != null)
                    {
                        libelle = lesson.Course.Title;
                    }
                    HourDay.Add(day, libelle);
                }
                agenda.Add(hour, HourDay);
            }

            return agenda;

        }
    }
}