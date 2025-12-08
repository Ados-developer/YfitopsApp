using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YfitopsApp.Entities;
using YfitopsApp.Interfaces;
using YfitopsApp.Models;

namespace YfitopsApp.Controllers
{
    [Authorize(Roles = "Admin,Artist")]
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly UserManager<User> _userManager;

        public TrackController(ITrackService trackService, UserManager<User> userManager)
        {
            _trackService = trackService;
            _userManager = userManager;
        }
        public IActionResult Create(int albumId)
        {
            var model = new TrackViewModel { AlbumId = albumId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrackViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var track = new Track
            {
                Title = model.Title,
                AlbumId = model.AlbumId
            };

            await _trackService.CreateAsync(track);
            TempData["Message"] = "The Track successfully created.";
            return RedirectToAction("Edit", "Album", new { id = model.AlbumId });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var track = await _trackService.GetByIdAsync(id);
            if (track == null) return NotFound();

            var model = new TrackViewModel
            {
                Id = track.Id,
                Title = track.Title,
                AlbumId = track.AlbumId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TrackViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Track? track = await _trackService.GetByIdAsync(model.Id);
            if (track == null) return NotFound();

            track.Title = model.Title;
            await _trackService.UpdateAsync(track);
            TempData["Message"] = "The Track successfully edited.";
            return RedirectToAction("Edit", "Album", new { id = track.AlbumId });
        }
        public async Task<IActionResult> Delete(int id)
        {
            Track? track = await _trackService.GetByIdAsync(id);
            if (track == null)
                return NotFound();

            TrackViewModel model = new TrackViewModel
            {
                Id = track.Id,
                Title = track.Title,
                AlbumId = track.AlbumId
            };
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Track? track = await _trackService.GetByIdAsync(id);
            if (track == null) return NotFound();

            await _trackService.DeleteAsync(id);
            TempData["Message"] = "The Track successfully deleted.";
            return RedirectToAction("Edit", "Album", new { id = track.AlbumId });
        }
    }
}
