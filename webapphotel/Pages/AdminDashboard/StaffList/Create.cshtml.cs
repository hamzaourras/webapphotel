using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.StaffList
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Staff Staff { get; set; }

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
            if (Staff.Name == Staff.PhoneNumber)
            {
                ModelState.AddModelError(string.Empty, "The Name cannot exactly match the Phone Number");
            }

            if (Staff.Address == null)
            {
                ModelState.AddModelError(string.Empty, "The address is required");
            }

            if (Staff.DateOfHirement == null)
            {
                ModelState.AddModelError("DateOfHirement", "The date of hirement is required");
            }

            if (Staff.Email == null || !Regex.IsMatch(Staff.Email, @"^[^@]+@[^@]+\.[^@]+$"))
            {
                ModelState.AddModelError("Email", "The Email is required and must be in a valid format (e.g., user@gmail.com).");
            }
            if (Staff.MonthlySalary == null || Staff.MonthlySalary <= 0)
            {
                ModelState.AddModelError(string.Empty, "The monthly salary must be a positive number.");
            }
            if (string.IsNullOrEmpty(Staff.JobTitle))
            {
                ModelState.AddModelError(string.Empty, "The job title is required.");
            }
            else if (Staff.JobTitle.Length > 100)
            {
                ModelState.AddModelError(string.Empty, "The job title cannot exceed 100 characters.");
            }



            if (ModelState.IsValid)
            {
                await _db.Staff.AddAsync(Staff);
                TempData["success"] = "Staff added successfuly";
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
