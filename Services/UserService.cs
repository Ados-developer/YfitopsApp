using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public Task<List<User>> GetArtistsAsync()
        {
            _logger.LogInformation("Request: All Artists.");
            return _userRepository.GetArtistsAsync();
        }

        public Task<User?> GetByIdAsync(string id)
        {
            _logger.LogInformation("Request: Artist");
            return _userRepository.GetByIdAsync(id);
        }
    }
}
