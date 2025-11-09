using SoldadosDoImperador.Models;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SoldadosDoImperador.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Conexao");

// Registra o �NICO DbContext: ContextoWarhammer
builder.Services.AddDbContext<ContextoWarhammer>(options =>
    options.UseSqlServer(connectionString));

// Registra o Identity e aponta para o ContextoWarhammer
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Inclui o RoleManager
    .AddEntityFrameworkStores<ContextoWarhammer>(); // Aponta para o DbContext correto

// Resto dos servi�os...
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

// Resto do pipeline...
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

// Bloco de Seed (Correto)
// (Certifique-se que o 'SeedData.cs' existe em 'Data/')
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // Removemos o try-catch temporariamente para debug
    await SeedData.InitializeAsync(services);
}

app.Run();