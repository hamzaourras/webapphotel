using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
            
        }

        public void OnGet()
        {
            // Optional: Add any initialization logic for the GET request
        }

        public async Task<IActionResult> OnPostAsync()
        {
             if (Category.Name == Category.PhoneNumber)
            {
                ModelState.AddModelError(string.Empty, "The Name cannot exactly match the Phone Number");
            }

            if (ModelState.IsValid)
            {
                await _db.Category.AddAsync(Category);
                TempData["success"] = "Client added successfuly";
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
