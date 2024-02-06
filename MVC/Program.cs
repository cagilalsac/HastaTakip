using Business.Services;
using Business.Utilities;
using Business.Utilities.Bases;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("tr-TR") // Ýngilizce: "en-US"
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

#region Connection String
var connectionString = builder.Configuration.GetConnectionString("Db");
#endregion

#region IoC Container
// Autofac, Ninject
// Unable to resolve service hatalarý burada çözümlenir
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IKlinikService, KlinikService>();
builder.Services.AddScoped<IBransService, BransService>();
builder.Services.AddScoped<IDoktorService, DoktorService>();
builder.Services.AddScoped<IHastaService, HastaService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();

builder.Services.AddSingleton<TcKimlikNoUtilBase, TcKimlikNoUtil>();

// AddScoped: istek (request) boyunca objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve yanýt (response) dönene kadar bu obje hayatta kalýr.
// AddSingleton: web uygulamasý baþladýðýnda objenin referansný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak)
// bir kere oluþturulur ve uygulama çalýþtýðý (IIS üzerinden uygulama durdurulmadýðý veya yeniden baþlatýlmadýðý) sürece bu obje hayatta kalýr.
// AddTransient: istek (request) baðýmsýz ihtiyaç olan objenin referansýný (genelde interface veya abstract class) kullandýðýmýz her yerde bu objeyi new'ler.
// Genelde AddScoped methodu kullanýlýr.
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

app.UseAuthorization();

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
