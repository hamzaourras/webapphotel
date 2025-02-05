using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;

        }

        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Category.Name == Category.PhoneNumber)
            {
                ModelState.AddModelError(string.Empty, "The Name cannot exactly match the Phone Number");
            }

            if (ModelState.IsValid)
            {
                _db.Category.Update(Category);
                TempData["success"] = "Client edited successfuly";
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
