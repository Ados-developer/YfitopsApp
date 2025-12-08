using System.ComponentModel.DataAnnotations;

namespace YfitopsApp.Models
{
    public class AlbumViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        public string ArtistId { get; set; } = string.Empty;
        [Display(Name = "Artist")]
        public string ArtistFullName { get; set; } = string.Empty;
        public List<TrackViewModel> Tracks { get; set; } = new();
        public bool IsFavorite { get; set; }
    }
}
