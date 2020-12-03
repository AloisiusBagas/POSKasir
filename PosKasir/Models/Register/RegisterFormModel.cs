using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Register
{
    public class RegisterFormModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        //[Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm Password not match!!")]
        public string ConfirmPassword { get; set; }
    }
}
