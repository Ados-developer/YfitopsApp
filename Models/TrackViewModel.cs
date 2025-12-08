namespace YfitopsApp.Models
{
    public class TrackViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AlbumId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
