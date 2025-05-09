using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapphotel.Data;
using webapphotel.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure the database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddSession();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760; // 10MB file upload
});

var app = builder.Build();


static async Task SeedRoles(IServiceProvider services)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var configuration = services.GetRequiredService<IConfiguration>();
    string[] roles = configuration.GetSection("Roles").Get<string[]>() ?? Array.Empty<string>();

    // Create missing roles
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Materialize roles to a list to avoid open data reader
    var existingRoles = roleManager.Roles.ToList();

    // Delete roles not in the configuration
    foreach (var role in existingRoles)
    {
        if (!roles.Contains(role.Name, StringComparer.OrdinalIgnoreCase))
        {
            await roleManager.DeleteAsync(role);
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    await SeedRoles(scope.ServiceProvider);
}



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // enable session if needed
app.UseAuthentication(); // REQUIRED for Identity
app.UseAuthorization(); // must come after Authentication

app.MapRazorPages();
app.Run();