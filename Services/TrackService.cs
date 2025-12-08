using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepo;
        private readonly ILogger<TrackService> _logger;

        public TrackService(ITrackRepository trackRepo, ILogger<TrackService> logger)
        {
            _trackRepo = trackRepo;
            _logger = logger;
        }
        public Task<List<Track>> GetAllTracksAsync()
        {
            _logger.LogInformation("Request: all tracks");
            return _trackRepo.GetAllAsync();
        }

        public Task<List<Track>> GetTracksByAlbumAsync(int albumId)
        {
            _logger.LogInformation("Request: tracks for album {AlbumId}", albumId);
            return _trackRepo.GetByAlbumIdAsync(albumId);
        }
        public async Task<Track?> GetByIdAsync(int id)
        {
            Track? track = await _trackRepo.GetByIdAsync(id);
            if (track == null)
                _logger.LogWarning("Track {Id} not found", id);
            return track;
        }
        public async Task CreateAsync(Track track)
        {
            await _trackRepo.AddAsync(track);
            await _trackRepo.SaveChangesAsync();
            _logger.LogInformation("Track '{Title}' created.", track.Title);
        }

        public async Task UpdateAsync(Track track)
        {
            await _trackRepo.UpdateAsync(track);
            await _trackRepo.SaveChangesAsync();
            _logger.LogInformation("Track {Id} updated", track.Id);
        }

        public async Task DeleteAsync(int id)
        {
            Track? track = await _trackRepo.GetByIdAsync(id);
            if (track == null)
            {
                _logger.LogError("Delete failed. Track {Id} does not exist", id);
                return;
            }

            await _trackRepo.DeleteAsync(track);
            await _trackRepo.SaveChangesAsync();
            _logger.LogInformation("Track {Id} deleted", id);
        }
    }
}
