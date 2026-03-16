using CRMSystem.Infrastructure;
using CRMSystem.Modules.Auth;
using CRMSystem.Modules.Customers;
using CRMSystem.Modules.Reports;
using CRMSystem.Modules.Tasks;
using CRMSystem.Shared.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddCustomersModule();
builder.Services.AddTaskModule();
builder.Services.AddReportModules();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication()
    .AddCookie("Cookies", options => 
    {
        options.LoginPath = "/Auth/Authorization/Authorization";
    });


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
//app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();