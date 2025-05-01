using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "AllowAllOrigins";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // This makes it work in Docker
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
builder.Services.AddOcelot();
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/", () => "Hello World!");
app.UseOcelot();

app.Run();
