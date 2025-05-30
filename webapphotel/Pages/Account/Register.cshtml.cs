using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Encodings.Web;
using webapphotel.Model;
using webapphotel.Services;
using webapphotel.ViewModels;

namespace webapphotel.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Users> userManager;
        private readonly IEmailService emailService;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(
            UserManager<Users> userManager,
            IEmailService emailService,
            ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.logger = logger;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Users users = new()
            {
                FullName = Input.Name,
                UserName = Input.Email,
                Email = Input.Email,
                EmailConfirmed = false
            };

            var result = await userManager.CreateAsync(users, Input.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(users, "User");
                
                // Generate email confirmation token
                var token = await userManager.GenerateEmailConfirmationTokenAsync(users);
                
                // Send confirmation email
                try
                {
                    await emailService.SendVerificationEmailAsync(users.Email, token);
                    
                    StatusMessage = "Registration successful! Please check your email to confirm your account.";
                    return RedirectToPage("/Account/RegisterConfirmation", new { email = Input.Email });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error sending email verification to user {Email}", users.Email);
                    ModelState.AddModelError(string.Empty, "Error sending verification email. Please try again.");
                    // Delete the user if we couldn't send the email
                    await userManager.DeleteAsync(users);
                    return Page();
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
