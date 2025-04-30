using DeliVeggieApp.WebApi.Models;
using DeliVeggieApp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliVeggieApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsRequestController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsRequestController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsRequestController>
        [HttpGet("GetProductList")]
        public IEnumerable<Product> GetProductList()
        {
            try
            {
                return _productService.GetAll();
            }
            catch
            {
                return null;
            }
           
        }

        // GET api/<ProductsRequestController>/5
        [HttpGet("GetProductDetails/{id}")]
        public Product GetProductDetails(int id)
        {
            try
            {
                Product product = _productService.GetById(id);
                product.Price = CalculatPriceDiscount(product.Price);
                return product;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private double CalculatPriceDiscount(double price)
        {
            int dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var priceReductions= _productService.GetPriceReductions();
            var priceReductionObj= priceReductions.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
            if (priceReductionObj != null)
            {
                price = price - (price * priceReductionObj.Reduction);
            }
          
            return price;
        }

        // POST api/<ProductsRequestController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ProductsRequestController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProductsRequestController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
