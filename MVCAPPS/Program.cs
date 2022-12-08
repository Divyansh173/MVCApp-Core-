using Coditas.ECommerce.Repositories;
using Coditas.ECommerce.Entities;
using Coditas.ECommerce.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using MVCAPPS.CustomFilters;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<eShoppingCodiContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddScoped<IDbAccessService<Category, int>, CategoryRepository>();
builder.Services.AddScoped<IDbAccessService<Product, int>, ProductDataAccess>();
builder.Services.AddScoped<IDbAccessService<SubCategory, int>, SubcategoryDataAccess>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(CustomLogRequestAttribute));
    options.Filters.Add(typeof(AppExceptionAttribute));
});
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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
