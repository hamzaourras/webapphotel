using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.AdminDashboard.ManageBooking
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        
        public Booking Booking { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Booking = _db.Bookings.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var bookingFromDb = _db.Bookings.Find(Booking.Id);
            if (bookingFromDb != null)
            {
                _db.Bookings.Remove(bookingFromDb);
                TempData["success"] = "Booking deleted successfully";
                await _db.SaveChangesAsync();
                return RedirectToPage("/AdminDashboard/ManageBooking/ManageBooking");
            }
            return Page();
        }
    }
}