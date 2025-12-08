using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;
using YfitopsApp.Models;

namespace YfitopsApp.Controllers
{
    [Authorize(Roles = "Admin,Artist")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AlbumController> _logger;
        private readonly ITrackService _trackService;
        public AlbumController(IAlbumService albumService, UserManager<User> userManager, ILogger<AlbumController> logger, ITrackService trackService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _logger = logger;
            _trackService = trackService;
        }
        public async Task<IActionResult> Index()
        {
            User? user = await _userManager.GetUserAsync(User);
            List<Album> albums;
            if (User.IsInRole("Admin"))
                albums = await _albumService.GetAllAlbumsAsync();
            else
                albums = await _albumService.GetAlbumsForArtistAsync(user!.Id);

            var model = albums.Select(a => new AlbumViewModel
            {
                Id = a.Id,
                ArtistId = a.ArtistId,
                Title = a.Title,
                ArtistFullName = a.Artist?.FullName ?? string.Empty,
            }).ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,Artist")]
        public IActionResult Create()
        {
            return View(new AlbumViewModel());
        }
        [Authorize(Roles = "Admin,Artist")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User? user = await _userManager.GetUserAsync(User);

            Album album = new Album
            {
                Title = model.Title,
                ArtistId = user!.Id,
            };

            await _albumService.CreateAsync(album);
            TempData["Message"] = "Album created successfully.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetByIdAsync(id);
            if (album == null)
                return NotFound();

            List<Track> tracks = await _trackService.GetTracksByAlbumAsync(album.Id);
            var model = new AlbumViewModel
            {
                Id = album.Id,
                Title = album.Title,
                ArtistId = album.ArtistId,
                Tracks = tracks.Select(t => new TrackViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    AlbumId = t.AlbumId
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlbumViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Album? existing = await _albumService.GetByIdAsync(model.Id);
            if (existing == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Artist") && !User.IsInRole("Admin") && existing.ArtistId != user!.Id)
                return Forbid();

            existing.Title = model.Title;

            if (User.IsInRole("Admin"))
                existing.ArtistId = model.ArtistId;

            await _albumService.UpdateAsync(existing);
            TempData["Message"] = "Album edited successfully.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            Album? album = await _albumService.GetByIdAsync(id);
            if (album == null)
                return NotFound();

            List<Track>? tracks = await _trackService.GetTracksByAlbumAsync(album.Id);

            AlbumViewModel model = new AlbumViewModel
            {
                Id = album.Id,
                Title = album.Title,
                ArtistId = album.ArtistId,
                Tracks = tracks.Select(t => new TrackViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    AlbumId = t.AlbumId
                }).ToList()
            };

            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Album? album = await _albumService.GetByIdAsync(id);
            if (album == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Artist") && !User.IsInRole("Admin") && album.ArtistId != user!.Id)
                return Forbid();

            var model = new AlbumViewModel
            {
                Id = album.Id,
                Title = album.Title,
                ArtistId = album.ArtistId,
            };

            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Album? album = await _albumService.GetByIdAsync(id);
            if (album == null)
                return NotFound();

            User? user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Artist") && !User.IsInRole("Admin") && album.ArtistId != user!.Id)
                return Forbid();

            await _albumService.DeleteAsync(id);
            TempData["Message"] = "Album removed successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
