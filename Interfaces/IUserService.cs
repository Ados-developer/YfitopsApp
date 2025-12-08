using YfitopsApp.Entities;

namespace YfitopsApp.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetArtistsAsync();
        Task<User?> GetByIdAsync(string id);
    }
}
