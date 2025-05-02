using VeggieAppConsole.Models;

namespace VeggieAppConsole.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<PriceReductions> GetPriceReductions();
    }
}
