using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }


        [Required (ErrorMessage ="Please enter your Email")]
        [Display (Name ="Email Address")]
        [EmailAddress (ErrorMessage ="Please enter a valid Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter your Password")]
        [Compare ("ConfirmPassword", ErrorMessage ="Password does not match")]
        [Display(Name = "Password")]
        [DataType (DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
