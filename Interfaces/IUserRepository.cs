using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetArtistsAsync();
        Task<User?> GetByIdAsync(string id);
    }
}
