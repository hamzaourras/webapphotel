namespace webapphotel.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public int FoundingYear { get; set; }
        public string Mission { get; set; }
        public string TeamDescription { get; set; }
        public string Awards { get; set; }
        public List<string> Services { get; set; } = new List<string>();
    }
}
