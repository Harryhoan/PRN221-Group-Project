using System.ComponentModel.DataAnnotations;

namespace PRN211GroupProject.ViewModel
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits long and contain only numbers")]
        public string Phone { get; set; }

    }
}
