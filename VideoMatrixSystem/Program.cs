using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using VideoMatrixSystem.Components;
using VideoMatrixSystem.Domain.Context;
using VideoMatrixSystem.Infraestructure.Repository;
using VideoMatrixSystem.Infraestructure.UseCases;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 36)),
//        opts => opts.EnableRetryOnFailure())
//    .LogTo(Console.WriteLine, LogLevel.Information));

string connectionString = "server=sql7.freesqldatabase.com;port=3306;database=sql7785603;user=sql7785603;password=A49JlLlHDt";

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<RepositoryManager>();
builder.Services.AddScoped<GesVideoMatrix>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();

//using (var db = new AppDbContext())
//{
//    db.ResetDatabase();
//}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
