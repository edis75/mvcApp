using Microsoft.EntityFrameworkCore;
using mvcApp.Data;
using mvcApp.Repository.Implementations;
using mvcApp.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ➤ EF Core + DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ➤ MVC
builder.Services.AddControllersWithViews();

// ➤ Session için gerekli servisler
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
// ➤ HttpContext erişimi gerekiyorsa
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 🔹 Middleware sıralaması
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();         // 🔹 Önce routing
app.UseSession();         // 🔹 Sonra session
app.UseAuthorization();   // 🔹 En son auth

// 🔹 Route tanımı EN SONDA olmalı
/* 
 * app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
