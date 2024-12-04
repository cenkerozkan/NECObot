using Npgsql;
using BLL.DAL;
using BLL.Services;
//using BLL.Services.Bases; (This is in comment line temporarily)
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Neco}/{action=Index}/{id?}");

app.Run();