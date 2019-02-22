using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.DTOModels;

namespace ContosoUniversity.Business
{
    public class StudentBL
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                return DbContext.Students.FirstOrDefault(s => s.ID == id);
            }
            catch (Exception)
            {

                return (null);
            }
        }
    }
}