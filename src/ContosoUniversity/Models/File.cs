using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class File
    {
        public int FileID { get; set; }

        public string Libelle { get; set; }

        public string Path { get; set; }

        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}