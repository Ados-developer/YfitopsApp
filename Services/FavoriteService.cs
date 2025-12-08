using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly ILogger<UserService> _logger;

        public FavoriteService(IFavoriteRepository favoriteRepo, ILogger<UserService> logger)
        {
            _favoriteRepo = favoriteRepo;
            _logger = logger;
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _favoriteRepo.GetByUserIdAsync(userId);
        }

        public async Task<bool> IsFavoriteAsync(string userId, string itemType, string itemId)
        {
            Favorite? fav = await _favoriteRepo.GetAsync(userId, itemType, itemId);
            return fav != null;
        }

        public async Task AddFavoriteAsync(string userId, string itemType, string itemId)
        {
            Favorite? exists = await _favoriteRepo.GetAsync(userId, itemType, itemId);
            if (exists == null)
            {
                await _favoriteRepo.AddAsync(new Favorite
                {
                    UserId = userId,
                    ItemType = itemType,
                    ItemId = itemId
                });
                await _favoriteRepo.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteAsync(string userId, string itemType, string itemId)
        {
            Favorite? fav = await _favoriteRepo.GetAsync(userId, itemType, itemId);
            if (fav != null)
            {
                await _favoriteRepo.RemoveAsync(fav);
                await _favoriteRepo.SaveChangesAsync();
            }
        }
    }
}
