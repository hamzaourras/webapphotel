
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;

namespace webapphotel.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Users> signInManager;

        public LogoutModel(SignInManager<Users> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            await signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }

    }
}
