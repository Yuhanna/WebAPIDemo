using Microsoft.Extensions.Options;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("ShirtsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7157/api/");//[Route("api/[controller]")]
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("AuthorityApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7157/");//[Route("[controller]")]
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;//because we do not want user to use script to modify the SESSION cookie
    options.IdleTimeout=TimeSpan.FromHours(5);
    options.Cookie.IsEssential = true; 
});

builder.Services.AddHttpContextAccessor();//To acces to here(session) from Custom Class directly

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IWebApiExecuter, WebApiExecuter>(); 

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

app.UseSession();//Session kullanmak için ilk burada tanýmlanýr

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
