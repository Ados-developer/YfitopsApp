using System.ComponentModel.DataAnnotations;

namespace YfitopsApp.Models
{
    public class EditAccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role")]
        public string SelectedRole { get; set; } = string.Empty;

        public List<string> AvailableRoles { get; set; } = new List<string> { "User", "Artist" };
    }
}
