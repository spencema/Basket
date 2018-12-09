using Checkout.Exceptions;
using Checkout.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkout.Services.Tests
{
    [TestClass]
    public class ReceiptServiceTest
    {
        private Mock<ICalculationService> _calculationServiceMock;
        private IReceiptService _receiptService;

        [TestInitialize]
        public void Init()
        {
            _calculationServiceMock = new Mock<ICalculationService>();
            _receiptService = new ReceiptService(_calculationServiceMock.Object);
        }

        [TestMethod]
        public void Print_A_ReturnsCorrectString()
        {
            string order = "A";
            decimal total = 50.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £50.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_B_ReturnsCorrectString()
        {
            string order = "B";
            decimal total = 30.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £30.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_C_ReturnsCorrectString()
        {
            string order = "C";
            decimal total = 20.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £20.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_D_ReturnsCorrectString()
        {
            string order = "D";
            decimal total = 15.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £15.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_AAA_ReturnsCorrectString()
        {
            string order = "AAA";
            decimal total = 130.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £130.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_BB_ReturnsCorrectString()
        {
            string order = "BB";
            decimal total = 45.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £45.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_Z_ReturnsErrorMessage()
        {
            string order = "Z";
            _calculationServiceMock.Setup(x => x.Total(order))
                .Throws(new ProductNotFoundException());
            try
            {
                _receiptService.Print(order);
            }
            catch (ProductNotFoundException exception)
            {
                string expected = "Could not process order for the following reason: 'Z' is not a known product. Please contact your store manager.";
                Assert.AreEqual(expected, exception.Message);
                _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
            }

        }

        [TestMethod]
        public void Print_EmptyString_ReturnsZero()
        {
            string order = string.Empty;
            decimal total = 00.00m;
            _calculationServiceMock.Setup(x => x.Total(order))
                .Returns(total);
            string expected = "Receipt\n" +
                              "-------\n" +
                              "Total: £0.00";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Once());
        }

        [TestMethod]
        public void Print_InvalidOrder_ReturnsErrorMessage()
        {
            string order = "A-A-A-A";
            string expected = $"Your order could not be processed as the following " +
                              $"order: {order} is not in a valid format.\n" +
                              $"Your order should be a string of store keeping units containing no " +
                              $"whitespace or delimeters, for example: " +
                              $"A, AA or ABB";
            string actual = _receiptService.Print(order);
            Assert.AreEqual(expected, actual);
            _calculationServiceMock.Verify(x => x.Total(order), Times.Never());
        }
    }
}
