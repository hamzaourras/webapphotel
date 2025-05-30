using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using webapphotel.Model;
using webapphotel.ViewModels;

namespace webapphotel.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public LoginModel(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the email exists
            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // Check if email is confirmed
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "You must verify your email before you can log in.");
                return RedirectToPage("/Account/RegisterConfirmation", new { email = Input.Email });
            }

            var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                    return RedirectToPage("/AdminDashboard/Index");
                else
                    return RedirectToPage("/UserDashboard/Index");
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("/Account/Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
    }
}
