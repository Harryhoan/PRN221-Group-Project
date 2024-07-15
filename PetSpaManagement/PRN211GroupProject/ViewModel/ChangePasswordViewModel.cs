using System.ComponentModel.DataAnnotations;

namespace PRN211GroupProject.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string OldPass { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,15}$", ErrorMessage = "Password must be from 8-15 characters, at least one uppercase letter, one lowercase letter, one number and one special character!")]
        public string NewPass { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPass", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
