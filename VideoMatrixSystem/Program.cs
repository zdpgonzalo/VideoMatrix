using Microsoft.EntityFrameworkCore;
using VideoMatrixSystem.Components;
using VideoMatrixSystem.Domain.Context;
using VideoMatrixSystem.Infraestructure.Repository;
using VideoMatrixSystem.Infraestructure.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)),
        opts => opts.EnableRetryOnFailure())
    .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddScoped<RepositoryManager>();
builder.Services.AddScoped<GesVideoMatrix>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
