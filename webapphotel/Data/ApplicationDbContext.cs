using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapphotel.Model;

namespace webapphotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Staff> Staff { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        
        public DbSet<Services> Services { get; set; }
    }
}
