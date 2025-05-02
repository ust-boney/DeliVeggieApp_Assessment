namespace VeggieAppConsole.Models
{
    public interface IProductStoreSettings
    {
        string ConnectionString { get; set; }
        string ProductsCollectionName { get; set; }
        string PriceReductionCollection { get; set; }
        string Database { get; set; }
    }
}