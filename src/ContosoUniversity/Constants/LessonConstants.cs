using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Constants
{
    public class LessonConstants
    {
        public const string ERROR_STARTHOUR = "StartHour must be between 8:00 and 18:00";
        public const string ERROR_ENDHOUR = "EndHour must be between 9:00 and 19:00";
        public const string ERRORS_STARTHOUR_AFTER_ENDHOUR = "Endhour must be after StartHour";
        public const string ERRORS_PLANNING = "You have already a course between";
    }
}