using Doctor_Attendance.Models;
using Doctor_Attendance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddSingleton<IDoctorRepository, SQLDoctorRepository>();

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


//bool isExpressEdition;
//using (var connection = new SqlConnection($"Data Source={Environment.MachineName};Initial Catalog=Doctor_Attendance;Integrated Security=True"))
//{
//    connection.Open();
//    var command = new SqlCommand("SELECT SERVERPROPERTY('EngineEdition') AS Edition", connection);
//    isExpressEdition = ((int)command.ExecuteScalar()) == 5;
//}

//string serverName = Environment.MachineName;
//string instanceName = isExpressEdition ? "SQLEXPRESS" : ""; // Replace with your instance name

//string connectionString = $"Data Source={serverName}\\{instanceName};Database=master;Initial Catalog=Doctor_Attendance;Integrated Security=True";

//builder.Services.AddDbContextPool<AppDBContext>(options =>
//{
//    options.UseSqlServer(connectionString);
//});



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

//Use Session
app.UseSession();

app.MapRazorPages();

app.Run();
