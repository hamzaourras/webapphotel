using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;
using webapphotel.ViewModels;

namespace webapphotel.Pages.Account
{
    public class VerifyEmailModel : PageModel
    {
        private readonly UserManager<Users> userManager;

        public VerifyEmailModel(UserManager<Users> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public VerifyEmailViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not found.");
                return Page();
            }

            return RedirectToPage("/Account/ForgotPassword", new { username = user.UserName });
        }
    }
}
