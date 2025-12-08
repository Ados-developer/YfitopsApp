using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YfitopsApp.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public Album? Album { get; set; }
    }
}
