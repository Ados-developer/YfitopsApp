using System.ComponentModel.DataAnnotations;

namespace YfitopsApp.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Invalid current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
