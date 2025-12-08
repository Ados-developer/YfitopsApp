namespace YfitopsApp.Models.Public
{
    public class ArtistAlbumsViewModel
    {
        public ArtistViewModel Artist { get; set; } = new();
        public List<AlbumViewModel> Albums { get; set; } = new();
    }
}
