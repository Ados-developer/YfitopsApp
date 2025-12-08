using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> GetArtistsAsync()
        {
            var artists = await _userManager.GetUsersInRoleAsync("Artist");
            return artists.ToList();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
