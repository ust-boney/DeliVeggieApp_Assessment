using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "AllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          //policy.WithOrigins("http://localhost:4200/")
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
