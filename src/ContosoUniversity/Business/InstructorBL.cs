using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Business
{
    public class InstructorBL
    {
        public Instructor GetInstructorById(int id)
        {
            try
            {
                return DBUtils.db.Instructors.Find(id);

            }
            catch (Exception)
            {

                return (null);
            }
        }
    }
}