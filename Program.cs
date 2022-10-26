using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Configuration:");
foreach (var item in app.Configuration.AsEnumerable())
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}

// print out container ip address
var host = app.Services.GetRequiredService<IHostApplicationLifetime>();
host.ApplicationStarted.Register(() =>
{
    var ip = Dns.GetHostAddresses(Dns.GetHostName())
        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
    Console.WriteLine($"Container IP: {ip}");
});

app.Run();