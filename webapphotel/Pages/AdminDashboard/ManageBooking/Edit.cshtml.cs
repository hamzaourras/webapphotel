using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.AdminDashboard.ManageBooking
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Booking Booking { get; set; } = new Booking();

        public IActionResult OnGet(int id)
        {
            var bookingFromDb = _db.Bookings.Find(id);
            if (bookingFromDb == null)
            {
                TempData["error"] = "Booking not found.";
                return RedirectToPage("/AdminDashboard/ManageBooking/ManageBooking");
            }

            Booking = bookingFromDb;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Booking.CheckOutDate <= Booking.CheckInDate)
            {
                ModelState.AddModelError(string.Empty, "Check-out date must be after check-in date.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Bookings.Update(Booking);
            await _db.SaveChangesAsync();
            TempData["success"] = "Booking edited successfully.";
            return RedirectToPage("/AdminDashboard/ManageBooking/ManageBooking");
        }
    }
}