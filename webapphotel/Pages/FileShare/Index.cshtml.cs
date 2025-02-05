using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;
using webapphotel.Data;
using System.Collections.Generic;
using System.Linq;

namespace webapphotel.Pages.FileShare  
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<FileModel> FileModels { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            FileModels = _context.FileModels.ToList(); 
        }
    }
}
