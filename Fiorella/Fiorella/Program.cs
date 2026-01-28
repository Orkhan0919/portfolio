using Fiorella.DAL;
using Fiorella.Models;
using Fiorella.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt=>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddIdentity<User, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 8;
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opt.Lockout.MaxFailedAccessAttempts = 5;
        opt.Lockout.AllowedForNewUsers = true;
        opt.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<AppDbContext>() 
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "admin",
    "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
);

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=Index}/{id?}"
);

app.Run();