using Microsoft.EntityFrameworkCore;
using YfitopsApp.Data;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly YfitopsDbContext _context;

        public TrackRepository(YfitopsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Track>> GetAllAsync()
        {
            return await _context.Tracks.Include(t => t.Album).ThenInclude(a => a!.Artist).ToListAsync();
        }
        public async Task<List<Track>> GetByAlbumIdAsync(int albumId)
        {
            return await _context.Tracks.Where(t => t.AlbumId == albumId).Include(t => t.Album).ThenInclude(a => a!.Artist).ToListAsync();
        }
        public async Task<Track?> GetByIdAsync(int id)
        {
            return await _context.Tracks.Include(t => t.Album).ThenInclude(a => a!.Artist).FirstOrDefaultAsync(t =>  t.Id == id);
        }
        public async Task AddAsync(Track track)
        {
            await _context.Tracks.AddAsync(track);
        }
        public Task UpdateAsync(Track track)
        {
            _context.Tracks.Update(track);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Track track)
        {
            _context.Tracks.Remove(track);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
