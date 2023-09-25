using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string serverName = Environment.MachineName;

builder.Services.AddDbContextPool<AppDBContext>(options =>
{
    options.UseSqlServer($"Data Source={serverName}\\SQLEXPRESS;Initial Catalog=Doctor_Attendance;Integrated Security=True; MultipleActiveResultSets=true");
});
//AddSession 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the idle timeout
    options.Cookie.HttpOnly = true; // Set HttpOnly option for the session cookie
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

builder.Services.AddRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Use Session
app.UseSession();

app.MapRazorPages();

app.Run();
