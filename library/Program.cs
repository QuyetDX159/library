using library.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

<<<<<<< HEAD

=======
>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
<<<<<<< HEAD
        option.LoginPath = "/Users/Login";
=======
        option.LoginPath = "/Access/Login";
>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContextCore")));

<<<<<<< HEAD
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourCookieName";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
=======
>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
<<<<<<< HEAD
app.UseSession();
=======

>>>>>>> f87de6b070b981742c3402a2b4c6ea9765cc3ee7
app.UseAuthentication();
app.UseCookiePolicy();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Login}/{id?}");

app.Run();
