using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
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



app.MapControllers();

app.MapGet("/api/hello", () => Results.Json(new { message = "Prateek new changeweewewweews" }));

app.MapGet("/config", (IConfiguration config) =>
{
    return new
    {
        ApiBaseUrl = config["ApiBaseUrl"],
        FeatureFlagX = config["FeatureFlagX"],
        ApiKey = config["ApiKey"]
    };
});



app.MapGet("/api/redis-test", async (IConfiguration config) =>
{
    var host = config["REDIS_HOST"];
    var port = config["REDIS_PORT"];
    var password = config["REDIS_PASSWORD"];

    try
    {
        var options = new ConfigurationOptions
        {
            EndPoints = { $"{host}:{port}" },
            Password = password,
            AbortOnConnectFail = true,
            ConnectTimeout = 5000
        };

        var redis = await ConnectionMultiplexer.ConnectAsync(options);
        var db = redis.GetDatabase();

        await db.StringSetAsync("operator:test", "Hello Operator");
        var value = await db.StringGetAsync("operator:test");

        return Results.Ok(new { value = value.ToString() });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
//commentss
// Serve the SPA/default page for any non-API route
app.UseStaticFiles();
app.Run("http://0.0.0.0:8080");