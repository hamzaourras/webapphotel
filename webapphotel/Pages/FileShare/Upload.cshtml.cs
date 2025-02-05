using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapphotel.Model;
using System.IO;
using System;
using webapphotel.Data;


namespace webapphotel.Pages.FileShare
{
public class UploadModel : PageModel
{
    private readonly IWebHostEnvironment _environment;
    private readonly ApplicationDbContext _context;

    public UploadModel(IWebHostEnvironment environment, ApplicationDbContext context)
    {
        _environment = environment;
        _context = context;
    }

    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UploadedFile != null)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, UploadedFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(stream);
            }

            var file = new FileModel
            {
                FileName = UploadedFile.FileName,
                FilePath = "/uploads/" + UploadedFile.FileName,
                UploadDate = DateTime.Now
            };

            _context.FileModels.Add(file);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}
}
