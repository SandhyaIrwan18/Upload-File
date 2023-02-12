using Upload_File.Interface;
using Upload_File.Interface.Implementation;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Transient services are always different, a new instance is created with every retrieval of the service.
// Scoped services change only with a new scope, but are the same instance within a scope.
// Singleton services are always the same, a new instance is only created once.
// For more clarification, check out https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage
builder.Services.AddTransient<IUploadFiles, UploadFiles>();

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
