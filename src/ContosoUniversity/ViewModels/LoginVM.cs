using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Login must have 6 Characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is Required"), DataType(DataType.Password)]
        [StringLength(64)]
        public string Password { get; set; }

    }
}