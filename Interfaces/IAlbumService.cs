using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAllAlbumsAsync();
        Task<List<Album>> GetAlbumsForArtistAsync(string artistId);
        Task<Album?> GetByIdAsync(int id);
        Task CreateAsync(Album album);
        Task UpdateAsync(Album album);
        Task DeleteAsync(int id);
    }
}
