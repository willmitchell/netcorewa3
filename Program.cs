using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck("HealthChecker", () => HealthCheckResult.Healthy("A healthy result."));

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.MapGet("/Environment", () =>
// {
//     return new EnvironmentInfo();
// });

// app.UseAuthorization();

app.MapControllers();

// enable debug logging
app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Path);
    await next();
});

// set global logging level to debug

Console.WriteLine("Configuration:");
foreach (var item in app.Configuration.AsEnumerable())
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}

// print out container ip address
// var host = app.Services.GetRequiredService<IHostApplicationLifetime>();
// host.ApplicationStarted.Register(() =>
// {
//     var ip = Dns.GetHostAddresses(Dns.GetHostName())
//         .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
//     Console.WriteLine($"Container IP: {ip}");
// });

// add a redirect from / to /swagger/index.html
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
    }
    else
    {
        await next();
    }
});

app.Run();