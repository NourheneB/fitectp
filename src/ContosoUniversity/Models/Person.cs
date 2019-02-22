using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ContosoUniversity.Models
{
    public abstract class Person
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is Required"), DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}