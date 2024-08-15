using InvesTechPlanner.Web.Components;
using InvesTechPlanner.Infrastructure.Repositories;
using InvesTechPlanner.UseCases.Interfaces;
using InvesTechPlanner.Infrastructure;
using InvesTechPlanner.UseCases;
using InvesTechPlanner.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Added manually
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Services
builder.Services.AddScoped<IDemandService, DemandService>();
builder.Services.AddScoped<IDemandDetailsService, DemandDetailsService>();

// Repositories
builder.Services.AddScoped<IDemandRepository, DemandRepository>();
builder.Services.AddScoped<IDemandDetailsRepository, DemandDetailsRepository>();

builder.Services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
builder.Services.AddScoped<OptionsService>();


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
