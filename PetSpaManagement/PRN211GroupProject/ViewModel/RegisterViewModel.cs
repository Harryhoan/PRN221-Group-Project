using System.ComponentModel.DataAnnotations;

namespace PRN211GroupProject.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain both letters and numbers")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Pass", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 11 digits long and contain only numbers")]
        public string Phone { get; set; }
    }

}
