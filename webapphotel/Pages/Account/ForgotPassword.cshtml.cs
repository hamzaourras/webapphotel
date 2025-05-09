using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using webapphotel.Model;
using webapphotel.ViewModels;

namespace webapphotel.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Users> userManager;

        public ForgotPasswordModel(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public ChangePasswordViewModel Input { get; set; }

        public IActionResult OnGet(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Account/VerifyEmail");
            }

            Input = new ChangePasswordViewModel { Email = username };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please fill out all required fields correctly.");
                return Page();
            }

            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found.");
                return Page();
            }

            // Generate reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Reset password using the token
            var result = await userManager.ResetPasswordAsync(user, token, Input.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToPage("/Account/Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

    }
}
