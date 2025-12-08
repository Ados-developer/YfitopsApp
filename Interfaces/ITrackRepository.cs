using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface ITrackRepository
    {
        Task<List<Track>> GetAllAsync();
        Task<List<Track>> GetByAlbumIdAsync(int albumId);
        Task<Track?> GetByIdAsync(int id);
        Task AddAsync(Track track);
        Task UpdateAsync(Track track);
        Task DeleteAsync(Track track);
        Task SaveChangesAsync();
    }
}
