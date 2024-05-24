using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Mention Valid Email")]

        public string Email { get; set; }


        public string UserType { get; }



        [Required(ErrorMessage = "Please Enter Correct Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password Does Not Match Please Enter Correct Password")]
        public string ConfirmPassword { get; set; }





        [StringLength(75)]
        [Required(ErrorMessage = "Please Enter The Permanent Address")]
        public string Address { get; set; }
    }
}

