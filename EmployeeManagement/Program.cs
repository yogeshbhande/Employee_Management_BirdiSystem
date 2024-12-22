using EmployeeManagement.Data;
using EmployeeManagement.Middleware;
using EmployeeManagement.Repositories.Implementation;
using EmployeeManagement.Repositories.Interfaces;
using EmployeeManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Register custom exception middleware

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/Employee/Index"); // Change "/CustomPage" to your desired page
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
