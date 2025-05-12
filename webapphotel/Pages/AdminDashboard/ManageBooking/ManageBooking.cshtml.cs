using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Data;
using webapphotel.Model;
using System.Collections.Generic;


namespace webapphotel.Pages.AdminDashboard.ManageBooking
{
    public class ManageBookingModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public List<Booking> BookingList { get; set; }

        public ManageBookingModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            BookingList = _db.Bookings.ToList();
        }
    }
}
