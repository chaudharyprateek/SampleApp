using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();

app.MapControllers();

app.MapGet("/api/hello", () => Results.Json(new { message = "Prateek" }));

app.MapGet("/config", (IConfiguration config) =>
{
    return new
    {
        ApiBaseUrl = config["ApiBaseUrl"],
        FeatureFlagX = config["FeatureFlagX"],
        ApiKey = config["ApiKey"]
    };
});
//comment
// Serve the SPA/default page for any non-API route
app.MapFallbackToFile("index.html");

app.Run("http://0.0.0.0:8080");