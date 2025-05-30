using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;
using webapphotel.Services;

namespace webapphotel.Pages.Account
{
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<Users> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<RegisterConfirmationModel> _logger;

        public RegisterConfirmationModel(
            UserManager<Users> userManager,
            IEmailService emailService,
            ILogger<RegisterConfirmationModel> logger)
        {
            _userManager = userManager;
            _emailService = emailService;
            _logger = logger;
        }

        [BindProperty]
        [TempData]
        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Index");
            }

            Email = email;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            try
            {
                await _emailService.SendVerificationEmailAsync(user.Email, token);
                StatusMessage = "Confirmation email has been resent. Please check your inbox.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resending verification email to {Email}", user.Email);
                StatusMessage = "Error resending verification email. Please try again later.";
            }

            return Page();
        }
    }
}