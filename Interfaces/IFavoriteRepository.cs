using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetByUserIdAsync(string userId);
        Task<Favorite?> GetAsync(string userId, string itemType, string itemId);
        Task AddAsync(Favorite favorite);
        Task RemoveAsync(Favorite favorite);
        Task SaveChangesAsync();
    }
}
