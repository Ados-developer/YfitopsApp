using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetUserFavoritesAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, string itemType, string itemId);
        Task AddFavoriteAsync(string userId, string itemType, string itemId);
        Task RemoveFavoriteAsync(string userId, string itemType, string itemId);
    }
}
