using DeliVeggieApp.WebApi.Models;
using DeliVeggieApp.WebApi.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggieApp.Tests
{
    [TestFixture]
    public class TestProductService
    {
        private IMongoClient? _mockClient;
        private IMongoDatabase? _mockDatabase;
        private IMongoCollection<Product>? _products;
        private IProductStoreSettings? _mockSettings;
        private ProductService? _productService;

        [SetUp]
        public void Setup()
        {
            _mockClient = Substitute.For<IMongoClient>();
            _mockDatabase = Substitute.For<IMongoDatabase>();
            _products = Substitute.For<IMongoCollection<Product>>();
            _mockSettings = Substitute.For<IProductStoreSettings>();

            _mockSettings.ConnectionString.Returns(string.Empty);
            _mockSettings.Database.Returns("TestDb");
            _mockSettings.ProductsCollectionName.Returns("products");
            _mockClient.GetDatabase("TestDb", null).Returns(_mockDatabase);
            _mockDatabase.GetCollection<Product>("products", null).Returns(_products);

        var fakeProducts = new List<Product>
        {
            new Product { Id="45ert", ItemId = 1, Name = "Product A", Price=40,EntityDate="" },
            new Product { Id="3455f", ItemId = 2, Name = "Product B", Price=25,EntityDate="" }
        };
            var mockCursor = Substitute.For<IAsyncCursor<Product>>();
            mockCursor.Current.Returns(fakeProducts);
            mockCursor.MoveNext(Arg.Any<CancellationToken>()).Returns(true, false);

            var mockFindFluent = Substitute.For<IFindFluent<Product, Product>>();
            mockFindFluent.ToCursor().Returns(mockCursor);
            //var mockFindFluent = Substitute.For<IFindFluent<Product, Product>>();
            //mockFindFluent.ToList().Returns(fakeProducts);

      //      _products
      //.Find(Arg.Any<FilterDefinition<Product>>(), Arg.Any<FindOptions<Product,Product >>())
      //.Returns(mockFindFluent);




            _productService = new ProductService(_mockSettings, _mockClient);
        }

        [TearDown]
        public void TearDown()
        {
            _mockClient?.Dispose();
            _mockClient=null;
            _products=null;
            _productService=null;
        }

        // check if there are atleast 2 documents in product collection
        [Test]
        public void ProductsCollection_ReturnsAtleastTwoDocuments()
        {
           var productList= _productService.GetAll();

            Assert.That(productList, Is.Not.Null);
            Assert.That(productList.Count,Is.AtLeast(2));
        }
        // check if the supplied documents have name, price
        // verify the price calculation works after applying price reduction
    }
}
