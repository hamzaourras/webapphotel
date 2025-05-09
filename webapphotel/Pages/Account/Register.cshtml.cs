using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;
using webapphotel.ViewModels;

namespace webapphotel.Pages.Account
{
    public class RegisterModel : PageModel
    {
        
        private readonly UserManager<Users> userManager;

        public RegisterModel( UserManager<Users> userManager)
        {
            
            this.userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

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
            };

            var result = await userManager.CreateAsync(users, Input.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(users, "User");
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
