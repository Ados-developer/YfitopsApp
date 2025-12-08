using Microsoft.EntityFrameworkCore;
using YfitopsApp.Data;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly YfitopsDbContext _context;

        public AlbumRepository(YfitopsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Album>> GetAllAsync()
        {
            return await _context.Albums.Include(a => a.Artist).ToListAsync();
        }
        public async Task<Album?> GetByIdAsync(int id)
        {
            return await _context.Albums.Include(a => a.Tracks).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Album>> GetByArtistIdAsync(string artistId)
        {
            return await _context.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }
        public async Task AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
        }
        public Task UpdateAsync(Album album)
        {
            _context.Albums.Update(album);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Album album)
        {
            _context.Albums.Remove(album);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
