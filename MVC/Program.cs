using Business.Services;
using Business.Utilities;
using Business.Utilities.Bases;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MVC.Utilities;
using MVC.Utilities.Bases;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("tr-TR") // İngilizce: "en-US"
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Hesaplar/Home/Login";
        config.LogoutPath = "/Hesaplar/Home/Logout";
        config.AccessDeniedPath = "/Hesaplar/Home/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });
#endregion

#region Session
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(60);
    config.IOTimeout = Timeout.InfiniteTimeSpan;
});
#endregion

#region Connection String
var connectionString = builder.Configuration.GetConnectionString("Db");
#endregion

#region IoC Container
// Autofac, Ninject
// Unable to resolve service hataları burada çözümlenir
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IKlinikService, KlinikService>();
builder.Services.AddScoped<IBransService, BransService>();
builder.Services.AddScoped<IDoktorService, DoktorService>();
builder.Services.AddScoped<IHastaService, HastaService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<UlkeServiceBase, UlkeService>();
builder.Services.AddScoped<SehirServiceBase, SehirService>();
builder.Services.AddScoped<IRaporService, RaporService>();

builder.Services.AddSingleton<TcKimlikNoUtilBase, TcKimlikNoUtil>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<FavoriDoktorlarSessionUtilBase, FavoriDoktorlarSessionUtil>();

// AddScoped: istek (request) boyunca objenin referansını (genelde interface veya abstract class) kullandığımız yerde obje (somut class'tan oluşturulacak)
// bir kere oluşturulur ve yanıt (response) dönene kadar bu obje hayatta kalır.
// AddSingleton: web uygulaması başladığında objenin referansnı (genelde interface veya abstract class) kullandığımız yerde obje (somut class'tan oluşturulacak)
// bir kere oluşturulur ve uygulama çalıştığı (IIS üzerinden uygulama durdurulmadığı veya yeniden başlatılmadığı) sürece bu obje hayatta kalır.
// AddTransient: istek (request) bağımsız ihtiyaç olan objenin referansını (genelde interface veya abstract class) kullandığımız her yerde bu objeyi new'ler.
// Genelde AddScoped methodu kullanılır.
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});
#endregion

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

#region Authentication
app.UseAuthentication(); // sen kimsin?
#endregion

app.UseAuthorization(); // sen neler yapabilirsin?

#region Session
app.UseSession();
#endregion

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
