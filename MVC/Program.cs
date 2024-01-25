using Business.Services;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Connection String
var connectionString = builder.Configuration.GetConnectionString("Db");
#endregion

#region IoC Container
// Autofac, Ninject
// Unable to resolve service hatalarý burada çözümlenir
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IKlinikService, KlinikService>();
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
