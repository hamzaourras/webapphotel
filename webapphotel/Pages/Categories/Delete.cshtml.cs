using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;

        }

        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var categoryFromDb = _db.Category.Find(Category.Id);
            if (categoryFromDb != null)
            {
                _db.Category.Remove(categoryFromDb);
                TempData["success"] = "Client deleted successfuly";
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
