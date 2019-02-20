using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class PersonVM
    {
        #region Person

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is Required"), DataType(DataType.Password)]
        [StringLength(64)]
        public string Password { get; set; }

     
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("NewPassword", ErrorMessage = "Password doesn't match.")]
        [StringLength(64)]
        public string ComfirmPassword { get; set; }
        #endregion
    }
}