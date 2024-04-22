using DemoRegAndLoginWithIdentity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});






//builder.Services.AddDbContext<MyDbContext>(options =>

//{
//    var connectionString = builder.Configuration.GetConnectionString("MySqlConn");
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

//});

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>();

//builder.Services.AddDbContext<MVCDemoDbContext>(options =>
//{
//    var connectionString1 = builder.Configuration.GetConnectionString("MvcDemoConnectionString");
//    options.UseMySql(connectionString1,ServerVersion.AutoDetect(connectionString1));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
