using DeliVeggieApp.WebApi.Models;

namespace DeliVeggieApp.WebApi.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<PriceReductions> GetPriceReductions();
    }
}
