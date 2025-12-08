using System.ComponentModel.DataAnnotations.Schema;

namespace YfitopsApp.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ArtistId { get; set; } = string.Empty;

        [ForeignKey(nameof(ArtistId))]
        public User? Artist { get; set; }

        public List<Track> Tracks { get; set; } = new();
    }
}
