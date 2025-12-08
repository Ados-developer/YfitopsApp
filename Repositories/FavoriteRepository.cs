using Microsoft.EntityFrameworkCore;
using YfitopsApp.Data;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly YfitopsDbContext _context;

        public FavoriteRepository(YfitopsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorite?> GetAsync(string userId, string itemType, string itemId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ItemType == itemType && f.ItemId == itemId.ToString());
        }

        public async Task AddAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
        }

        public Task RemoveAsync(Favorite favorite)
        {
            _context.Favorites.Remove(favorite);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
