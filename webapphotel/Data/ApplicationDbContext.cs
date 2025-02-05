using Microsoft.EntityFrameworkCore;
using webapphotel.Model;

namespace webapphotel.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Staff>Staff { get; set; }

        public DbSet<FileModel> FileModels { get; set; }
        public IEnumerable<FileModel> FileModel { get; internal set; }
    }
}
