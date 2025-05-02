using MongoDB.Driver;
using VeggieAppConsole.Models;
namespace VeggieAppConsole.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<PriceReductions> _priceReductions;
        public ProductService(IProductStoreSettings productStoreSettings, IMongoClient mongoClient)
        {
            var databaseName = mongoClient.GetDatabase(productStoreSettings.Database);
            _products = databaseName.GetCollection<Product>(productStoreSettings.ProductsCollectionName);
            _priceReductions= databaseName.GetCollection<PriceReductions>(productStoreSettings.PriceReductionCollection);

        }
        public List<Product> GetAll()
        {
          return  _products.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetById(int id)
        {
            return _products.Find(p=> p.ItemId == id).FirstOrDefault();
        }

        public List<PriceReductions> GetPriceReductions()
        {
            return _priceReductions.Find(FilterDefinition<PriceReductions>.Empty).ToList();
        }
    }
}
