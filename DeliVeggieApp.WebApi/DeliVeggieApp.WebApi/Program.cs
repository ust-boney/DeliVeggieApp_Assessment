using DeliVeggieApp.WebApi.Models;
using DeliVeggieApp.WebApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.Configure<ProductStoreSettings>(builder.Configuration.GetSection(nameof(ProductStoreSettings)));
builder.Services.AddSingleton<IProductStoreSettings>(sp => sp.GetRequiredService<IOptions<ProductStoreSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("ProductStoreSettings:ConnectionString")));
builder.Services.AddScoped<IProductService,ProductService>();

//setting policy for cross origin
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

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
