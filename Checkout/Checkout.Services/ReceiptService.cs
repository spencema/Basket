using Checkout.Exceptions;
using Checkout.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Checkout.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly ICalculationService _calculationService;
        private readonly string _receiptMessage = "Receipt\n" +
                                                  "-------\n" +
                                                  "Total: {0}";
        private readonly string _errorMessage = "Could not process order for the following reason: {0}";
        private readonly string _invalidFormatErrorMessage = "Your order could not be processed as the following " +
                                                             "order: {0} is not in a valid format.\n" +
                                                             "Your order should be a string of store keeping units containing no " +
                                                             "whitespace or delimeters, for example: " +
                                                             "A, AA or ABB";

        public ReceiptService(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        public string Print(string order)
        {
            (bool valid, string error) = ValidateOrder(order);
            if (!valid)
            {
                return error;
            }

            try
            {
                decimal total = _calculationService.Total(order);
                string receipt = string.Format(_receiptMessage, total.ToString("C2"));
                return receipt;
            }
            catch (ProductNotFoundException exception)
            {
                return string.Format(_errorMessage, exception.Message);
            }
        }

        private (bool valid, string error) ValidateOrder(string order)
        {
            // An empty string is valid as it will return a price of zero.
            if (order == string.Empty)
            {
                return (valid: true, error: null);
            }

            string lettersOnlyPattern = "^[A-Z]+$";
            var regex = new Regex(lettersOnlyPattern);
            if (!regex.IsMatch(order))
            {
                return (valid: false, error: string.Format(_invalidFormatErrorMessage, order));
            }

            return (valid: true, error: null);
        }
    }
}
