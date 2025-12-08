using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAsync();
        Task<Album?> GetByIdAsync(int id);
        Task<List<Album>> GetByArtistIdAsync(string artistId);
        Task AddAsync(Album album);
        Task UpdateAsync(Album album);
        Task DeleteAsync(Album album);
        Task SaveChangesAsync();
    }
}
