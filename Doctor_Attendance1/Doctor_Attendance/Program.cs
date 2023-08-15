using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddSingleton<IDoctorRepository, SQLDoctorRepository>();

builder.Services.AddDbContextPool<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBconnectionstring"));
});
//adding identity framework here
/*
builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<AppDBContext>()
        .AddDefaultTokenProviders();
*/
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

app.MapRazorPages();

app.Run();
