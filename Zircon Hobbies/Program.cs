using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Data;
using Microsoft.Extensions.DependencyInjection;
using Zircon_Hobbies.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;


//var config = new ConfigurationBuilder()
//    .AddJsonFile($"appsettings.json", true, true)
//    .AddJsonFile($"appsettings.{environment}.json", true, true)
//    .AddEnvironmentVariables()
//    .AddUserSecrets(Program)()
//    .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Zircon_HobbiesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Zircon_HobbiesContext") ?? throw new InvalidOperationException("Connection string 'Zircon_HobbiesContext' not found.")));

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthorization().AddGoogle(options =>
//{
//    IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentiction:Google");
//    options.ClientId = googleAuthNSection["ClientId"];
//    options.ClientSecret = googleAuthNSection["ClientSecret"];
//});


var app = builder.Build();

app.UseSession();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Identityseeder.SeedDefaultData(scope.ServiceProvider);
    ProductionCompanySeedData.Initializer(services);
    GunplaSeedData.Initialize(services);


}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.MapRazorPages();

app.Run();
