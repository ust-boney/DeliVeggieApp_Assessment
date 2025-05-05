using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using VeggieAppConsole;
using Microsoft.Extensions.DependencyInjection;
using VeggieAppConsole.Services;
using VeggieAppConsole.Models;
using VeggieAppConsole.MessageBroker;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((context, config) =>
           {
               config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
           })        
            .ConfigureServices((context, services) => { 
                services.Configure<ProductStoreSettings>(context.Configuration.GetSection(nameof(ProductStoreSettings)));
                services.AddSingleton<IProductStoreSettings>(sp => sp.GetRequiredService<IOptions<ProductStoreSettings>>().Value);
                services.AddSingleton<IMongoClient>(s =>
                 new MongoClient(context.Configuration.GetValue<string>("ProductStoreSettings:ConnectionString")));
                services.AddScoped<IProductService, ProductService>();
                services.AddHostedService<Subscriber>();
            }).Build();

 await host.RunAsync();
 
        // publish message from Console app
        //using var scope = builder.Services.CreateScope();
        //var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
        //var products = productService.GetAll();
        //string jsonResult = JsonSerializer.Serialize(products);
        //var publisher = new Publisher();
        //publisher.SendMessage(jsonResult);
        //Console.ReadLine();




    }
}