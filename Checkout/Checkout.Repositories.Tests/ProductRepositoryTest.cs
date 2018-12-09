using Checkout.Exceptions;
using Checkout.Interfaces.Repositories;
using Checkout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout.Repositories.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private IProductRepository _productRepository;

        [TestInitialize]
        public void Init()
        {
            _productRepository = new ProductRepository();
        }
 
        [TestMethod]
        public void Get_A_ReturnsCorrectProduct()
        {
            Product expected = new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 };
            Product actual = _productRepository.Get('A');
            Assert.AreEqual(expected.IsMultibuy, actual.IsMultibuy);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.PriceDiscounted, actual.PriceDiscounted);
            Assert.AreEqual(expected.MultibuyQuantity, actual.MultibuyQuantity);
        }

        [TestMethod]
        public void Get_B_ReturnsCorrectProduct()
        {
            Product expected = new Product() { IsMultibuy = true, Price = 30.00m, PriceDiscounted = 45.00m, MultibuyQuantity = 2 };
            Product actual = _productRepository.Get('B');
            Assert.AreEqual(expected.IsMultibuy, actual.IsMultibuy);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.PriceDiscounted, actual.PriceDiscounted);
            Assert.AreEqual(expected.MultibuyQuantity, actual.MultibuyQuantity);
        }

        [TestMethod]
        public void Get_C_ReturnsCorrectProduct()
        {
            Product expected = new Product() { Price = 20.00m };
            Product actual = _productRepository.Get('C');
            Assert.AreEqual(expected.IsMultibuy, actual.IsMultibuy);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.PriceDiscounted, actual.PriceDiscounted);
            Assert.AreEqual(expected.MultibuyQuantity, actual.MultibuyQuantity);
        }

        [TestMethod]
        public void Get_D_ReturnsCorrectProduct()
        {
            Product expected = new Product() { Price = 15.00m };
            Product actual = _productRepository.Get('D');
            Assert.AreEqual(expected.IsMultibuy, actual.IsMultibuy);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.PriceDiscounted, actual.PriceDiscounted);
            Assert.AreEqual(expected.MultibuyQuantity, actual.MultibuyQuantity);
        }

        [TestMethod]
        public void Get_E_ReturnsCorrectProduct()
        {
            Product expected = new Product() { IsMultibuy = true, Price = 25.00m, PriceDiscounted = 25.00m, MultibuyQuantity = 2 };
            Product actual = _productRepository.Get('E');
            Assert.AreEqual(expected.IsMultibuy, actual.IsMultibuy);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.PriceDiscounted, actual.PriceDiscounted);
            Assert.AreEqual(expected.MultibuyQuantity, actual.MultibuyQuantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void Get_ProductThatDoesntExist_ThrowsException()
        {
            try
            {
                Product actual = _productRepository.Get('Z');
            }
            catch (ProductNotFoundException exception)
            {
                string expected = "'Z' is not a known product. Please contact your store manager.";
                string actual = exception.Message;
                Assert.AreEqual(expected, actual);
                throw;
            }
        }
    }
}
