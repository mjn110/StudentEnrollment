using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using StudentEnrolment.Server.Interfaces;
using StudentEnrolment.Server.Services;
using StudentEnrolment.Shared.Data;
using Microsoft.Extensions.DependencyInjection;
using StudentEnrolment.Shared.Models; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#region Context  
builder.Services.AddDbContext<EnrolmentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region IOC  
builder.Services.AddTransient<IStudentService, StudentService>();
#endregion

#region Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<EnrolmentContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<SignInManager<ApplicationUser>>(); // Ensure SignInManager is registered
builder.Services.AddScoped<UserManager<ApplicationUser>>(); // Ensure UserManager is registered
#endregion  

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.  
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); // Add this line  
app.UseAuthorization(); // Add this line  

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
