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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "Password must be from 8-15 characters, at least one uppercase letter, one lowercase letter, one number and one special character!")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Pass", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits long and contain only numbers")]
        public string Phone { get; set; }
    }

}
