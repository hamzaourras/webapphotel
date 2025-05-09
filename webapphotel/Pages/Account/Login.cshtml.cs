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

        public LoginModel(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                var user = await signInManager.UserManager.FindByEmailAsync(Input.Email);
                var roles = await signInManager.UserManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                    return RedirectToPage("/AdminDashboard/Index");
                else
                    return RedirectToPage("/UserDashboard/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }


}
