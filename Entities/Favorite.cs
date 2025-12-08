using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YfitopsApp.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
