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
    public class LessonBL
    {
        private SchoolContext db = new SchoolContext();

        public IEnumerable<Lesson> GetAllLessons()
        {
            return this.db.Lessons;
        }

        public Instructor GetInstructor(int id)
        {
            return db.Instructors.Find(id);
        }


        public Lesson GetLesson(int? id)
        {
            return db.Lessons.Find(id);
        }

        public Lesson CreateLesson(Instructor instructorConnected, CourseDay Day, string Course, DateTime StartHour, DateTime EndHour, DateTime DateStart)
        {
                Lesson lesson = new Lesson();
                lesson.InstructorID = instructorConnected.ID;
                lesson.Day = Day;
                lesson.CourseID = int.Parse(Course);
                lesson.StartHour = this.HarmonizeDatetime(StartHour);
                lesson.EndHour = this.HarmonizeDatetime(EndHour);
                lesson.DateStart = DateStart;
                return lesson;

        }

        public void EditLesson(Lesson lesson, LessonEditVM lessonEdited)
        {
            lesson.DateStart = lessonEdited.DateStart;
            lesson.Day = lessonEdited.Day;
            lesson.EndHour = lessonEdited.EndHour;
            lesson.StartHour = lessonEdited.StartHour;
            db.SaveChanges();
        }

        public void AddLesson(Lesson lesson)
        {
            using (this.db)
            {
                db.Lessons.Add(lesson);
                db.SaveChanges();
            }
        }

        public void DeleteLesson(Lesson lesson)
        {
            using (this.db)
            {
                db.Lessons.Remove(lesson);
                db.SaveChanges();
            }
        }

        public bool IsPlanningCreationValid(Lesson lesson)
        {
            int count = db.Lessons.Where(l => l.InstructorID == lesson.InstructorID)
                        .Where(l => l.Day == lesson.Day).Where(l => (l.StartHour >= lesson.StartHour && l.StartHour <= lesson.EndHour)
                        || (l.EndHour <= lesson.EndHour && l.EndHour >= lesson.StartHour)).ToList().Count;

            return (count == 0);
        }

        public bool IsPlanningEditValid(Lesson lesson)
        {
            int count = db.Lessons.Where(l => l.InstructorID == lesson.InstructorID)
                        .Where(l => l.LessonID != lesson.LessonID)
                        .Where(l => l.Day == lesson.Day).Where(l => (l.StartHour >= lesson.StartHour && l.StartHour <= lesson.EndHour)
                        || (l.EndHour <= lesson.EndHour && l.EndHour >= lesson.StartHour)).ToList().Count;

            return (count == 0);
        }

        public DateTime HarmonizeDatetime(DateTime dateToHarmonize)
        {
            return new DateTime(1970, 1, 1, dateToHarmonize.Hour, dateToHarmonize.Minute, 0);
        }



    }
}