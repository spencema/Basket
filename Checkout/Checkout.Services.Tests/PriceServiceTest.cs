using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkout.Interfaces.Services;
using Checkout.Exceptions;
using Moq;
using Checkout.Interfaces.Repositories;
using Checkout.Models;

namespace Checkout.Services.Tests
{
    [TestClass]
    public class PriceServiceTest
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private IPriceService _priceService;

        [TestInitialize]
        public void Init()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _priceService = new PriceService(_productRepositoryMock.Object);
        }

        [TestMethod]
        public void GetPrice_A_Returns50()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 });
            var expected = 50.00m;
            decimal actual = _priceService.GetPrice('A');
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_B_Returns30()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 30.00m, PriceDiscounted = 45.00m, MultibuyQuantity = 2 });
            var expected = 30.00m;
            decimal actual = _priceService.GetPrice('B');
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_C_Returns20()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { Price = 20.00m });
            var expected = 20.00m;
            decimal actual = _priceService.GetPrice('C');
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_D_Returns15()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { Price = 15.00m });
            var expected = 15.00m;
            decimal actual = _priceService.GetPrice('D');
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_AQuantity2_Returns100()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 });
            var expected = 100.00m;
            int quantity = 2;
            decimal actual = _priceService.GetPrice('A', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_AQuantity3_Returns130()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 });
            var expected = 130.00m;
            int quantity = 3;
            decimal actual = _priceService.GetPrice('A', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_AQuantity4_Returns180()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 });
            var expected = 180.00m;
            int quantity = 4;
            decimal actual = _priceService.GetPrice('A', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_BQuantity2_Returns45()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 30.00m, PriceDiscounted = 45.00m, MultibuyQuantity = 2 });
            var expected = 45.00m;
            int quantity = 2;
            decimal actual = _priceService.GetPrice('B', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_BQuantity3_Returns75()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 30.00m, PriceDiscounted = 45.00m, MultibuyQuantity = 2 });
            var expected = 75.00m;
            int quantity = 3;
            decimal actual = _priceService.GetPrice('B', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        public void GetPrice_AQuantity0_Returns0()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 50.00m, PriceDiscounted = 130.00m, MultibuyQuantity = 3 });
            var expected = decimal.Zero;
            int quantity = 0;
            decimal actual = _priceService.GetPrice('A', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Never());
        }

        [TestMethod]
        public void GetPrice_EQuantity2_Returns25()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Returns(new Product() { IsMultibuy = true, Price = 25.00m, PriceDiscounted = 25.00m, MultibuyQuantity = 2 });
            var expected = 25.00m;
            int quantity = 2;
            decimal actual = _priceService.GetPrice('E', quantity);
            Assert.AreEqual(expected, actual);
            _productRepositoryMock.Verify(x => x.Get(It.IsAny<char>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void GetPrice_ProductThatDoesntExist_ThrowsException()
        {
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<char>()))
                .Throws(new ProductNotFoundException("'Z' is not a known product. Please contact your store manager."));
            try
            {
                _priceService.GetPrice('Z');
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
