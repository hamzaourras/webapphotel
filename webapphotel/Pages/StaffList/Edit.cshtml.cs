using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.StaffList
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Staff Staff { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // Handles GET requests
        public IActionResult OnGet(int id)
        {
            Staff = _db.Staff.Find(id);

            if (Staff == null)
            {
                TempData["error"] = "Staff not found.";
                return RedirectToPage("Index");
            }

            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (Staff.Name == Staff.PhoneNumber)
            {
                ModelState.AddModelError(string.Empty, "The Name cannot exactly match the Phone Number.");
            }

           
            if (string.IsNullOrWhiteSpace(Staff.Email) || !Staff.Email.Contains("@"))
            {
                ModelState.AddModelError("Staff.Email", "A valid email is required.");
            }

            if (Staff.MonthlySalary < 0)
            {
                ModelState.AddModelError("Staff.MonthlySalary", "Monthly Salary must be a positive number.");
            }

            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            var staffInDb = _db.Staff.Find(Staff.Id);

            if (staffInDb == null)
            {
                TempData["error"] = "Staff not found.";
                return RedirectToPage("Index");
            }

           
            staffInDb.Name = Staff.Name;
            staffInDb.PhoneNumber = Staff.PhoneNumber;
            staffInDb.Address = Staff.Address;
            staffInDb.DateOfHirement = Staff.DateOfHirement;
            staffInDb.Email = Staff.Email;
            staffInDb.JobTitle = Staff.JobTitle;
            staffInDb.MonthlySalary = Staff.MonthlySalary;

            
            try
            {
                await _db.SaveChangesAsync();
                TempData["success"] = "Staff edited successfully.";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
