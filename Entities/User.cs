using Microsoft.AspNetCore.Identity;

namespace YfitopsApp.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public List<Album> Albums { get; set; } = new();
    }
}
