using System.ComponentModel.DataAnnotations;

namespace MVC.Demo03.PL.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="email is required")]
        [EmailAddress(ErrorMessage ="invaild email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5,ErrorMessage ="MIN password length is 5 ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password) , ErrorMessage ="doesent match with password")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
