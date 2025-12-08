namespace YfitopsApp.Models.Public
{
    public class AlbumTracksViewModel
    {
        public AlbumViewModel Album { get; set; } = new();
        public List<TrackViewModel> Tracks { get; set; } = new();
    }
}
