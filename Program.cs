using Microsoft.EntityFrameworkCore;
using Midterm_EquipmentRental.Data;

var builder = WebApplication.CreateBuilder(args);

/* Set up In-memory database */
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MidtremDb-DanyKo-GabrielSiewert"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

/* Ensure database is successfully created */
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

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
