using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.ManageBooking
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;


        public Booking Booking { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            Booking = _db.Bookings.Find(id);

            if (Booking == null)
            {
                TempData["error"] = "Booking not found.";
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Sample validation (can be customized)
            if (Booking.FullName == Booking.PhoneNumber)
            {
                ModelState.AddModelError(string.Empty, "Full Name cannot be the same as Phone Number.");
            }

            if (string.IsNullOrWhiteSpace(Booking.Email) || !Booking.Email.Contains("@"))
            {
                ModelState.AddModelError("Booking.Email", "A valid email is required.");
            }

            if (Booking.CheckOutDate <= Booking.CheckInDate)
            {
                ModelState.AddModelError(string.Empty, "Check-Out must be after Check-In date.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bookingInDb = _db.Bookings.Find(Booking.Id);
            if (bookingInDb == null)
            {
                TempData["error"] = "Booking not found.";
                return RedirectToPage("Index");
            }

            bookingInDb.FullName = Booking.FullName;
            bookingInDb.Email = Booking.Email;
            bookingInDb.PhoneNumber = Booking.PhoneNumber;
            bookingInDb.RoomType = Booking.RoomType;
            bookingInDb.RoomNumber = Booking.RoomNumber;
            bookingInDb.CheckInDate = Booking.CheckInDate;
            bookingInDb.CheckOutDate = Booking.CheckOutDate;

            try
            {
                await _db.SaveChangesAsync();
                TempData["success"] = "Booking updated successfully.";
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