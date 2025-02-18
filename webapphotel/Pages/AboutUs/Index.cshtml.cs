using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapphotel.Pages.AboutUs
{
    public class IndexModel : PageModel
    {
        public string HotelName { get; set; } = "Grand Luxe Hotel";
        public string Location { get; set; } = "Marrakech, Morocco";
        public int FoundingYear { get; set; } = 1998;
        public string Mission { get; set; } = "Our mission is to provide an exceptional stay with world-class service and comfort.";
        public List<string> Services { get; set; } = new List<string>
        {
            "Luxury Rooms & Suites",
            "Fine Dining & Rooftop Restaurant",
            "Spa & Wellness Center",
            "Conference & Event Halls",
            "24/7 Concierge Service"
        };
        public string TeamDescription { get; set; } = "Our dedicated team ensures that every guest enjoys a memorable experience.";
        public string Awards { get; set; } = "Recognized as 'Best Boutique Hotel 2023' by Travel Magazine.";

        public void OnGet() { }
    }
}
