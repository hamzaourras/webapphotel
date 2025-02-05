using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.StaffList
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Staff Staff { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;

        }

        public void OnGet(int id)
        {
            Staff = _db.Staff.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var StaffFromDb = _db.Staff.Find(Staff.Id);
            if (StaffFromDb != null)
            {
                _db.Staff.Remove(StaffFromDb);
                TempData["success"] = "Staff deleted successfuly";
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
