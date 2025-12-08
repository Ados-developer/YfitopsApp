using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System;
using YfitopsApp.Entities;
using YfitopsApp.Models;

namespace YfitopsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel
            {
                AvailableRoles = new List<string> { "User", "Artist" }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} successfully registered.", model.Email);
                await _userManager.AddToRoleAsync(user, model.SelectedRole);
                _logger.LogInformation("User {Email} added to role {SelectedRole}", model.Email, model.SelectedRole);
                return RedirectToAction("Login");
            }
            _logger.LogError("Registration failed for {Email}. Errors: {Errors}",
            model.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User? user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed. User {Email} not found.", model.Email);
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
           var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} logged in successfully.", model.Email);
                return RedirectToAction("Index", "Account");
            }

            _logger.LogWarning("Invalid login attempt for user {Email}.", model.Email);
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Logout successful.");
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User? user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                _logger.LogError("Password change failed. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }
            _logger.LogInformation("Password successfully changed.");
            TempData["Message"] = "Password changed successfully";
            return RedirectToAction("Index", "Account");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            return View(user);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError("Delete account failed. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(user);
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("Account successfully deleted.");
            TempData["Message"] = "Your account has been deleted successfully.";
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var roles = await _userManager.GetRolesAsync(user);
            var currentRole = roles.FirstOrDefault() ?? "User";

            EditAccountViewModel model = new EditAccountViewModel
            {
                Email = user.Email!,
                FullName = user.FullName,
                SelectedRole = currentRole
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User? user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
            }
            user.FullName = model.FullName;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                _logger.LogError("Account failed to update. Errors: {Errors}", string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(model.SelectedRole))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                var roleResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Account roles failed to update. Errors: {Errors}", string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    foreach (var error in roleResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }

            _logger.LogInformation("Account details updated successfully.");
            TempData["Message"] = "Account details updated successfully.";
            return RedirectToAction("Index", "Account");
        }

    }
}
