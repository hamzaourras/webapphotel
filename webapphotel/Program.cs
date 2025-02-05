using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using webapphotel.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure the database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configure file upload size limit (default is 2MB, increase if needed)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760; // 10MB limit
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
