using VeggieAppConsole.Models;

namespace VeggieAppConsole.Services
{
    public class ProductStoreSettings :IProductStoreSettings
    {
        public string ConnectionString { get; set; }
        public string ProductsCollectionName { get; set; }
        public string PriceReductionCollection { get; set; }
        public string Database { get; set; }

    }
}
