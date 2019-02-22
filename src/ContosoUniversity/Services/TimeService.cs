using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Services
{
    public class TimeService
    {
        public static string GetHourFromDate(DateTime date)
        {
            return $"{date.Hour}:{date.Minute}";
        }
    }
}