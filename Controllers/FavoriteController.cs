using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;

namespace YfitopsApp.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<User> _userManager;

        public FavoriteController(IFavoriteService favoriteService, UserManager<User> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(string itemType, string itemId)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            bool isFav = await _favoriteService.IsFavoriteAsync(user.Id, itemType, itemId);
            if (isFav)
            {
                await _favoriteService.RemoveFavoriteAsync(user.Id, itemType, itemId);
            }
            else
            {
                await _favoriteService.AddFavoriteAsync(user.Id, itemType, itemId);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
