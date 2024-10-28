using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MVC;
using MVC.ApiService;
using MVC.Models;
using MVC.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<AdministradorService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddTransient<ClienteService>();
builder.Services.AddTransient<CanchaService>(); 
builder.Services.AddTransient<TurnoService>();

builder.Services.AddHttpContextAccessor(); // Agrega esta l�nea


builder.Services.AddHttpClient(); // Registra HttpClient

// Agregar servicios de sesi�n
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de espera de la sesi�n
    options.Cookie.HttpOnly = true; // La cookie solo se puede acceder a trav�s de HTTP
    options.Cookie.IsEssential = true; // La cookie es esencial para la aplicaci�n
});


builder.Services.AddHttpContextAccessor();

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

// Aseg�rate de que UseSession est� antes de UseAuthorization
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}");

app.Run();
