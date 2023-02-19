using Prometheus;
using DucksAgency.Spygame.Clientportal.Healthchecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.RegisterHealthChecks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.MapHealthChecksEndpoints();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapMetrics();
app.MapFallbackToPage("/_Host");

app.Run();
