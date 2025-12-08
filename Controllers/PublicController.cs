using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;
using YfitopsApp.Models;
using YfitopsApp.Models.Public;
using YfitopsApp.Services;

namespace YfitopsApp.Controllers
{
    [Authorize]
    public class PublicController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAlbumService _albumService;
        private readonly ITrackService _trackService;
        private readonly UserManager<User> _userManager;
        private readonly IFavoriteService _favService;

        public PublicController(IUserService userService, IAlbumService albumService, ITrackService trackService, UserManager<User> userManager, IFavoriteService favService)
        {
            _userService = userService;
            _albumService = albumService;
            _trackService = trackService;
            _userManager = userManager;
            _favService = favService;
        }
        public async Task<IActionResult> Artists()
        {
            User? user = await _userManager.GetUserAsync(User);
            List<User> artists = await _userService.GetArtistsAsync();
            List<Favorite> userFavorites = await _favService.GetUserFavoritesAsync(user!.Id);
            List<ArtistViewModel> model = artists.Select(a => new ArtistViewModel
            {
                Id = a.Id,
                FullName = a.FullName,
                IsFavorite = userFavorites.Any(f => f.ItemId == a.Id && f.ItemType == "Artist")
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Albums(string artistId)
        {
            User? user = await _userManager.GetUserAsync(User);
            User? artist = await _userService.GetByIdAsync(artistId);
            if (artist == null)
                return NotFound();
            List<Favorite> userFavorites = await _favService.GetUserFavoritesAsync(user!.Id);
            List<Album> albums = await _albumService.GetAlbumsForArtistAsync(artistId);
            ArtistAlbumsViewModel model = new ArtistAlbumsViewModel
            {
                Artist = new ArtistViewModel
                {
                    Id = artist.Id,
                    FullName = artist.FullName
                },
                Albums = albums.Select(a => new AlbumViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    IsFavorite = userFavorites.Any(f => f.ItemId == a.Id.ToString() && f.ItemType == "Album")
                }).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Tracks(int albumId)
        {
            User? user = await _userManager.GetUserAsync(User);
            Album? album = await _albumService.GetByIdAsync(albumId);
            if (album == null)
                return NotFound();
            List<Favorite> userFavorites = await _favService.GetUserFavoritesAsync(user!.Id);
            List<Track> tracks = await _trackService.GetTracksByAlbumAsync(albumId);
            AlbumTracksViewModel model = new AlbumTracksViewModel
            {
                Album = new AlbumViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                },
                Tracks = tracks.Select(t => new TrackViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsFavorite = userFavorites.Any(f => f.ItemId == t.Id.ToString() && f.ItemType == "Track")
                }).ToList()
            };

            return View(model);
        }
    }
}
