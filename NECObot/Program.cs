using Npgsql;
using BLL.DAL;
using BLL.Services;
using BLL.Services.Bases;
using BLL.Models;
using DotnetGeminiSDK;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration
    //.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register the services
builder.Services.AddScoped<IService<ChatThread, ChatThreadModel>, ChatThreadService>();
builder.Services.AddScoped<IService<Message, MessageModel>, MessagesService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

// Add gemini service in here.

// Add DbContext service with Npgsql
builder.Services.AddDbContext<BLL.DAL.Db>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));



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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Neco}/{action=Index}/{id?}");

app.Run();