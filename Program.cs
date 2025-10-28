using SoldadosDoImperador.Models;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SoldadosDoImperador.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Conexao");


builder.Services.AddDbContext<ContextoWarhammer>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ContextoWarhammer>();
builder.Services.ConfigureApplicationCookie(options =>
{
  
    options.Cookie.HttpOnly = true; 

    
    options.ExpireTimeSpan = TimeSpan.FromMinutes(2);

   
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    
    options.SlidingExpiration = true;
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US") };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

app.UseRequestLocalization();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}



app.Run(); 