using Checkout.Exceptions;
using Checkout.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Services.Tests
{
    [TestClass]
    public class CalculationServiceTest
    {
        private Mock<IPriceService> _priceServiceMock;
        private ICalculationService _calculationService;

        [TestInitialize]
        public void Init()
        {
            _priceServiceMock = new Mock<IPriceService>();
            _calculationService = new CalculationService(_priceServiceMock.Object);
        }

        [TestMethod]
        public void Total_A_Returns50()
        {
            char expectedStoreKeepingUnit = 'A';
            int expectedQuantity = 1;
            decimal expected = 50.00m;
            _priceServiceMock.Setup(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity)).Returns(expected);
            decimal actual = _calculationService.Total("A");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity), Times.Once());
        }

        [TestMethod]
        public void Total_B_Returns30()
        {
            char expectedStoreKeepingUnit = 'B';
            int expectedQuantity = 1;
            decimal expected = 30.00m;
            _priceServiceMock.Setup(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity)).Returns(expected);
            decimal actual = _calculationService.Total("B");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity), Times.Once());
        }

        [TestMethod]
        public void Total_C_Returns20()
        {
            char expectedStoreKeepingUnit = 'C';
            int expectedQuantity = 1;
            decimal expected = 20.00m;
            _priceServiceMock.Setup(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity)).Returns(expected);
            decimal actual = _calculationService.Total("C");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity), Times.Once());
        }

        [TestMethod]
        public void Total_D_Returns15()
        {
            char expectedStoreKeepingUnit = 'D';
            int expectedQuantity = 1;
            decimal expected = 15.00m;
            _priceServiceMock.Setup(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity)).Returns(expected);
            decimal actual = _calculationService.Total("D");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(expectedStoreKeepingUnit, expectedQuantity), Times.Once());
        }

        [TestMethod]
        public void Total_AB_Returns80()
        {
            int expectedQuantity = 1;
            decimal APrice = 50.00m;
            decimal BPrice = 30.00m;
            _priceServiceMock.SetupSequence(x => x.GetPrice(It.IsAny<char>(), expectedQuantity))
                .Returns(APrice)
                .Returns(BPrice);
            decimal expected = 80.00m;
            decimal actual = _calculationService.Total("AB");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), expectedQuantity), Times.Exactly(2));
        }

        [TestMethod]
        public void Total_CDBA_Returns115()
        {
            int expectedQuantity = 1;
            decimal CPrice = 20.00m;
            decimal DPrice = 15.00m;
            decimal BPrice = 30.00m;
            decimal APrice = 50.00m;
            _priceServiceMock.SetupSequence(x => x.GetPrice(It.IsAny<char>(), expectedQuantity))
                .Returns(CPrice)
                .Returns(DPrice)
                .Returns(BPrice)
                .Returns(APrice);
            decimal expected = 115.00m;
            decimal actual = _calculationService.Total("CDBA");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), expectedQuantity), Times.Exactly(4));
        }

        [TestMethod]
        public void Total_AA_Returns100()
        {
            int expectedQuantity = 2;
            _priceServiceMock.Setup(x => x.GetPrice(It.IsAny<char>(), expectedQuantity))
                .Returns(100.00m);
            decimal expected = 100.00m;
            decimal actual = _calculationService.Total("AA");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), expectedQuantity), Times.Once());
        }

        [TestMethod]
        public void Total_AAA_Returns130()
        {
            _priceServiceMock.Setup(x => x.GetPrice(It.IsAny<char>(), 3))
                .Returns(130.00m);
            decimal expected = 130.00m;
            decimal actual = _calculationService.Total("AAA");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), 3), Times.Once());
        }

        [TestMethod]
        public void Total_AAABB_Returns175()
        {
            decimal AAAPrice = 130.00m;
            decimal BBPrice = 45.00m;
            _priceServiceMock.SetupSequence(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(AAAPrice)
                .Returns(BBPrice);
            decimal expected = 175.00m;
            decimal actual = _calculationService.Total("AAABB");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Total_Null_Returns0()
        {
            decimal expected = decimal.Zero;
            decimal actual = _calculationService.Total(null);
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public void Total_EmptyString_Returns0()
        {
            decimal expected = decimal.Zero;
            decimal actual = _calculationService.Total(string.Empty);
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public void Total_WhiteSpace_Returns0()
        {
            decimal expected = decimal.Zero;
            decimal actual = _calculationService.Total(" ");
            Assert.AreEqual(expected, actual);
            _priceServiceMock.Verify(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void Total_ProductThatDoesntExist_PropagatesException()
        {
            _priceServiceMock.SetupSequence(x => x.GetPrice(It.IsAny<char>(), It.IsAny<int>()))
                .Throws(new ProductNotFoundException("'Z' is not a known product. Please contact your store manager."));
            _calculationService.Total("Z");
        }
    }
}
