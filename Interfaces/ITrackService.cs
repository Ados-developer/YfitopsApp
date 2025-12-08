using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface ITrackService
    {
        Task<List<Track>> GetAllTracksAsync();
        Task<List<Track>> GetTracksByAlbumAsync(int albumId);
        Task<Track?> GetByIdAsync(int id);
        Task CreateAsync(Track track);
        Task UpdateAsync(Track track);
        Task DeleteAsync(int id);
    }
}
