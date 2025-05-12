using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using webapphotel.Data;
using webapphotel.Model;

namespace webapphotel.Pages.AdminDashboard.ManageBooking
{

    public class AddBookingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

      
public AddBookingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();
            return RedirectToPage("/AdminDashboard/ManageBooking/ManageBooking");
        }
    }
}
