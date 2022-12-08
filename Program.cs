using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddHealthChecks()
//     .AddCheck("HealthChecker", () => HealthCheckResult.Healthy("A healthy result."));

// enable blazor pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// add a handler at / that returns HTML
app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(@"
    <html>
    <head>
        <title>ASP.NET Core Demo App</title>
    </head>
    <body>
        <a href=""/swagger"">Swagger</a>
    </body>
    </html>
    ");
});

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

// enable debug logging
app.Use(async (context, next) =>
{
    Console.WriteLine(context.Request.Path);
    await next();
});

Console.WriteLine("Configuration:");
foreach (var item in app.Configuration.AsEnumerable())
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}

var host = app.Services.GetRequiredService<IHostApplicationLifetime>();
host.ApplicationStarted.Register(() =>
{
    var ip = Dns.GetHostAddresses(Dns.GetHostName())
        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
    Console.WriteLine($"Container IP: {ip}");
});

// do not do the below, as it breaks the default health check.  
// easier to have / return a 200 and some content
// add a redirect from / to /swagger/index.html
// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/")
//     {
//         context.Response.Redirect("/swagger/index.html");
//     }
//     else
//     {
//         await next();
//     }
// });

app.Run();
