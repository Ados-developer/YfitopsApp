using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepo;
        private readonly ILogger<AlbumService> _logger;

        public AlbumService(IAlbumRepository repo, ILogger<AlbumService> logger)
        {
            _albumRepo = repo;
            _logger = logger;
        }
        public Task<List<Album>> GetAllAlbumsAsync()
        {
            _logger.LogInformation("Request: all albums.");
            return _albumRepo.GetAllAsync();
        }
        public Task<List<Album>> GetAlbumsForArtistAsync(string artistId)
        {
            _logger.LogInformation("Artist {ArtistId} request: their albums.", artistId);
            return _albumRepo.GetByArtistIdAsync(artistId);
        }
        public async Task<Album?> GetByIdAsync(int id)
        {
            Album? album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
                _logger.LogWarning("Album with Id {Id} not found.", id);
            else
                _logger.LogInformation("Loaded album {Id}: {Title}", album.Id, album.Title);

            return album;
        }
        public async Task CreateAsync(Album album)
        {
            _logger.LogInformation("Creating album '{Title}' for Artist {ArtistId}",
                album.Title, album.ArtistId);

            await _albumRepo.AddAsync(album);
            await _albumRepo.SaveChangesAsync();

            _logger.LogInformation("Album '{Title}' successfully created.", album.Title);
        }
        public async Task UpdateAsync(Album album)
        {
            _logger.LogInformation("Updating album {Id}: {Title}",
                album.Id, album.Title);

            await _albumRepo.UpdateAsync(album);
            await _albumRepo.SaveChangesAsync();

            _logger.LogInformation("Album {Title} successfully updated.", album.Title);
        }
        public async Task DeleteAsync(int id)
        {
            Album? album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                _logger.LogWarning("Delete failed. Album with Id {Id} does not exist.", id);
                return;
            }

            _logger.LogInformation("Deleting album {Id}: {Title}", id, album.Title);

            await _albumRepo.DeleteAsync(album);
            await _albumRepo.SaveChangesAsync();

            _logger.LogInformation("Album {Title} successfully deleted.", album.Title);
        }
    }
}
