using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;

namespace webapphotel.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;

        public ConfirmEmailModel(
            UserManager<Users> userManager,
            ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public bool IsConfirmed { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                IsConfirmed = false;
                ErrorMessage = "Invalid email verification link.";
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found during email confirmation.", email);
                IsConfirmed = false;
                ErrorMessage = "User not found.";
                return Page();
            }

            if (user.EmailConfirmed)
            {
                IsConfirmed = true;
                return Page();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                _logger.LogInformation("Email for user {UserId} confirmed successfully.", user.Id);
                IsConfirmed = true;
                return Page();
            }
            else
            {
                _logger.LogWarning("Failed to confirm email for user {UserId}. Errors: {Errors}",
                    user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
                IsConfirmed = false;
                ErrorMessage = "Email confirmation failed. The verification link may be invalid or expired.";
                return Page();
            }
        }
    }
}