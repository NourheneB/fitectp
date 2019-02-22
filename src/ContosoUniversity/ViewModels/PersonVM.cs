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

        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Login must have 6 Characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        #endregion
    }
}
