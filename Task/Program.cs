using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectiostring = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No Connection String Was Found ");

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectiostring));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITeachersRepository, TeachersRepository>();
builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();

builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

#region AddIdentity

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

#endregion



#region AddSession
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});
#endregion

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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
