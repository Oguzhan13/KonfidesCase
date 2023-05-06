var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("url", url => url.BaseAddress = new Uri("https://localhost:7230/api/"));
builder.Services.AddHttpClient("admin-url", url => url.BaseAddress = new Uri("https://localhost:7230/admin/"));
builder.Services.AddHttpClient("user-url", url => url.BaseAddress = new Uri("https://localhost:7230/user/"));

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
